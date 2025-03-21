using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteSelector : MonoBehaviour
{
    [Serializable] 
    public class NoteSpritePair
    {
        public Note note;
        public Sprite sprite;
    }

    [Serializable]
    public class NoteAudioPair 
    {
        public Note note;
        public AudioClip audioClip;
    }
    public enum Note
    {
        Rest, C, D, E, F, G, A, B, Cb
    }

    public List<Note> availableNotes = new List<Note>
    {
        Note.Rest, Note.C, Note.D, Note.E, Note.F, Note.G, Note.A, Note.B, Note.Cb
    };

    public Note currentNote;
    private int currentNoteIndex = 0;

    public TextMeshProUGUI noteDisplay;
    public RectTransform noteRectTransform;
    public Image selectorImage;

    public float moveOffset = 20f;
    private Vector3 initialPosition;
    private float minPositionY;
    private bool isSelected = false;

    public List<NoteSpritePair> noteSprites = new List<NoteSpritePair>();
    private SpriteRenderer spriteRenderer; // Add this line

    public List<NoteAudioPair> noteAudioClips = new List<NoteAudioPair>();
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeNote(int direction)
    {
        Debug.Log("ChangeNote() called with direction: " + direction);
        Debug.Log("Current Note Index (Before): " + currentNoteIndex);

        currentNoteIndex += direction;
        Debug.Log("Current Note Index (After Increment): " + currentNoteIndex);


        if (currentNoteIndex < 0)
        {
            currentNoteIndex = 0;
            Debug.Log("Current Note Index (Clamped to 0): " + currentNoteIndex);
        }
        else if (currentNoteIndex >= availableNotes.Count)
        {
            currentNoteIndex = availableNotes.Count - 1;
            Debug.Log("Current Note Index (Clamped to Max): " + currentNoteIndex);
        }
        currentNote = availableNotes[currentNoteIndex];
        Debug.Log("Current Note Index: " + currentNoteIndex);

        

        if (isSelected)
        {
            MoveNotePosition(direction);
        }
        PlayNoteSound();
    }

    void MoveNotePosition(int direction)
    {
        if (noteRectTransform != null)
        {
            Vector3 currentPosition = noteRectTransform.anchoredPosition;
            float targetY = currentPosition.y + direction * moveOffset;

            float maxPositionY = initialPosition.y + (availableNotes.Count - 1) * moveOffset;
            if (targetY < initialPosition.y)
            {
                targetY = initialPosition.y;
            }
            else if (targetY > maxPositionY)
            {
                targetY = maxPositionY;
            }
            else
            {
                targetY = Mathf.Max(targetY, minPositionY);
            }

            noteRectTransform.anchoredPosition = new Vector3(currentPosition.x, targetY, currentPosition.z);
        }
    }

    void ResetNotePosition()
    {
        if (noteRectTransform != null)
        {
            noteRectTransform.anchoredPosition = initialPosition;
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if (selectorImage != null)
        {
            selectorImage.enabled = selected;
        }
    }

    void PlayNoteSound()
    {
        if (audioSource != null)
        {
            foreach (NoteAudioPair pair in noteAudioClips)
            {
                if (pair.note == currentNote)
                {
                    audioSource.clip = pair.audioClip;
                    audioSource.Play();
                    break;
                }
            }
        }
    }

    public void PlayCurrentNoteSound(int i)
    {
        if (audioSource != null)
        {
            foreach (NoteAudioPair pair in noteAudioClips)
            {
                if (pair.note == currentNote)
                {
                    audioSource.clip = pair.audioClip;
                    audioSource.Play();
                    break;
                }

            }
        }
    }

    public void ResetToRest()
    {
        currentNoteIndex = 0;
        currentNote = availableNotes[currentNoteIndex];
        ResetNotePosition();
    }
}