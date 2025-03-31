/*
 * Name: Jack Gu
 * Date: 3/27/25
 * Desc: Defines and tracks the Notes of a Chord
 */

using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using ValuePitchEnums;

public class Chord : MonoBehaviour
{
    [Tooltip("The horizontal offset on change in Value, should be half of the distance between two Notes.")]
    public int horizontalOffset = 75;

    public GameObject notePrefab;

    // Stores own Chord index in Song
    private int index;

    // Note Audio Sources in order
    private List<AudioSource> audioSources;

    private Song song;

    private List<Note> notes;

    private Value value = Value.Quarter;

    private int focus = 0;

    // Start is called before the first frame update
    void Start()
    {
        song = GetComponentInParent<Song>();
        notes.Add(transform.GetChild(0).GetComponent<Note>());
        audioSources.Add(GetComponentInChildren<AudioSource>());
    }

    public void Reset()
    {
        while (value != Value.Quarter)
        {
            ChangeValue();
        }

        notes[0].Reset();
    }

    public void Selected()
    {
        notes[focus].Selected((int) value);
    }

    public void DeSelected()
    {
        notes[focus].DeSelected((int) value);
    }

    public int GetPitch()
    {
        return notes[focus].GetPitch();
    }

    // Used by Song to set index of Chord during Start
    public void SetIndex(int index)
    {
        this.index = index;
    }

    // Moves Note Pitch up by 1
    public void NoteUp()
    {
        bool up = false;
        int note = focus + 1;
        for (int pitch = notes[focus].GetPitch() + 1; Enum.IsDefined(typeof(Pitch), pitch); pitch++)
        {
            if (note >= notes.Count || notes[note++].GetPitch() > pitch)
            {
                up = true;
                break;
            }
        }

        if (up)
        {
            bool offset = false;

            if (++note < notes.Count - 1 && notes[note + 1].GetPitch() == notes[note].GetPitch() + 2)
            {
                offset = true;
            }

            while (note > focus)
            {
                notes[note].NoteDown();
                notes[note--].Offset(offset);
                offset = !offset;
            }
        }
        else
        {
            if (focus < notes.Count - 1)
            {
                focus++;
            }
        }
    }

    // Moves focused Note Pitch down by 1, checks for Notes underneath
    public void NoteDown()
    {
        bool down = false;
        int note = focus - 1;
        for (int pitch = notes[focus].GetPitch() - 1; pitch >= 0; pitch--)
        {
            if (note < 0 || notes[note--].GetPitch() < pitch)
            {
                down = true;
                break;
            }
        }

        if (down)
        {
            bool offset = false;

            if (++note > 0 && notes[note - 1].GetPitch() == notes[note].GetPitch() - 2)
            {
                offset = true;
            }

            while (note <= focus)
            {
                notes[note].NoteDown();
                notes[note++].Offset(offset);
                offset = !offset;
            }
        }
        else
        {
            if (focus > 0)
            {
                focus--;
            }
        }
    }

    // Cycles between Values
    public void ChangeValue()
    {
        // Reset size and position
        transform.localScale /= (int) value;
        transform.localPosition -= new Vector3((int) value / 2 * horizontalOffset, 0);

        // Remove outline
        notes[focus].DeSelected((int) value);

        switch (value)
        {
            case Value.Quarter:
                value = Value.Half;
                break;

            case Value.Half:
                value = Value.Quarter;
                break;
        }

        // Hides/reveals Chord based on Value
        if (index % (int) value == 0)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

        foreach (Note note in notes)
        {
            note.SetSprite(song.GetSprite((int) value, note.GetPitch()));
        }

        // Re-add outline
        notes[focus].Selected((int) value);

        // Size and position
        transform.localPosition += new Vector3((int) value / 2 * horizontalOffset, 0);
        transform.localScale *= (int) value;
    }

    public bool IsEnabled()
    {
        return gameObject.activeSelf;
    }
    public int GetValue()
    {
        return (int) value;
    }

    public void PlayChord(double time)
    {
        foreach (Note note in notes)
        {
            audioSources[focus].clip = song.GetAudioClip((int) value, note.GetPitch());
            audioSources[focus].PlayScheduled(time);
            audioSources[focus].SetScheduledEndTime(time + (double) value * 60 / song.GetTempo());
        }
    }

    public Sprite GetSprite(int pitch)
    {
        return song.GetSprite((int) value, pitch);
    }

    public void SetFocus(int focus)
    {
        notes[this.focus].DeSelected((int) value);
        this.focus = focus;
        notes[this.focus].Selected((int)value);
        song.SetFocus(index);
    }

    public void AddNote()
    {
        GameObject newNote = Instantiate(notePrefab, notes[focus].transform.position, Quaternion.identity, transform);
        SetFocus(++focus);
        notes.Insert(focus, newNote.GetComponent<Note>());

        for (int i = focus + 1; i < notes.Count; i++)
        {
            notes[i].index = i;
        }

        NoteUp();

        if (newNote.transform.position == notes[focus - 1].transform.position)
        {
            Destroy(newNote);
        }
    }

    public void RemoveNote()
    {
        Destroy(notes[focus].gameObject);
        notes.RemoveAt(focus);
        SetFocus(--focus);

        for (int i = focus; i < notes.Count; i++)
        {
            notes[i].index = i;
        }
    }
}
