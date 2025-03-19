using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class NoteManager : MonoBehaviour
{
    public List<NoteSelector> noteObjects;
    public List<NoteSelector.Note> correctTune = new List<NoteSelector.Note>
    {
        NoteSelector.Note.Rest, NoteSelector.Note.C, NoteSelector.Note.D, NoteSelector.Note.E, NoteSelector.Note.F, NoteSelector.Note.G,NoteSelector.Note.A, NoteSelector.Note.B, NoteSelector.Note.Cb
    };

    public TextMeshProUGUI feedbackText;

    private int selectedNoteIndex = 0;

    void Start()
    {
        GenerateRandomTune();
        if (noteObjects.Count > 0)
        {
            SelectNote(0);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeSelectedNote(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeSelectedNote(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (noteObjects.Count > 0)
            {
                noteObjects[selectedNoteIndex].ChangeNote(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (noteObjects.Count > 0)
            {
                noteObjects[selectedNoteIndex].ChangeNote(-1);
            }
        }
    }

    void ChangeSelectedNote(int direction)
    {
        selectedNoteIndex += direction;

        if (selectedNoteIndex < 0)
        {
            selectedNoteIndex = noteObjects.Count - 1;
        }
        else if (selectedNoteIndex >= noteObjects.Count)
        {
            selectedNoteIndex = 0;
        }

        SelectNote(selectedNoteIndex);
    }

    void SelectNote(int index)
    {
        if (noteObjects.Count > 0)
        {
            for (int i = 0; i < noteObjects.Count; i++)
            {
                noteObjects[i].SetSelected(i == index);
            }
        }
    }

    public void PlayAllNotes()
    {
        StartCoroutine(PlayNotesSequentially());
    }

    IEnumerator PlayNotesSequentially()
    {
      for (int i = 0; i < noteObjects.Count; i++)
        {
            noteObjects[i].PlayCurrentNoteSound(i);
            yield return new WaitForSeconds(0.5f);
        }
        CheckTune();
    }

    void CheckTune()
    {
        List<NoteSelector.Note> playerTune = new List<NoteSelector.Note>();
        foreach (NoteSelector noteSelector in noteObjects)
        {
            playerTune.Add(noteSelector.currentNote);
        }
        if (CompareTunes(playerTune, correctTune))
        {
            feedbackText.text = "Correct Tune!";
            GenerateRandomTune();

            foreach (NoteSelector noteSelector in noteObjects)
            {
                noteSelector.ResetToRest();
            }
        }
        else
        {
            feedbackText.text = "Incorrect Tune!";
        }
    }

    bool CompareTunes(List<NoteSelector.Note> playerTune, List<NoteSelector.Note> correctTune)
    {
        if (playerTune.Count != correctTune.Count)
        {
            return false;
        }

        for (int i = 0; i < playerTune.Count; i++)
        {
            if (playerTune[i] != correctTune[i])
            {
                return false;
            }
        }
        return true;
    }

    void GenerateRandomTune()
    {
        correctTune.Clear();
        for (int i = 0; i < noteObjects.Count; i++)
        {
            NoteSelector.Note randomNote = (NoteSelector.Note)Random.Range(0, System.Enum.GetValues(typeof(NoteSelector.Note)).Length);
            correctTune.Add(randomNote);
        }
        Debug.Log("New Tune Generated: " + string.Join(", ", correctTune));
    }

    public void PlayCorrectTune()
    {
        StartCoroutine(PlayCorrectTuneSequentially());
    }

    IEnumerator PlayCorrectTuneSequentially()
    {
        for (int i = 0; i < correctTune.Count; i++)
        {
            NoteSelector.Note note = correctTune[i];
            NoteSelector noteSelector = FindNoteSelectorByNote(note);
            if (noteSelector != null)
            {
                noteSelector.PlayCurrentNoteSound(i);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    NoteSelector FindNoteSelectorByNote(NoteSelector.Note note)
    {
        foreach (NoteSelector noteSelector in noteObjects)
        {
            if (noteSelector.currentNote == note)
            {
                return noteSelector;
            }
        }
        return null;
    }

}
