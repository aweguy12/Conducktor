/*
 * Name: Jack Gu
 * Date: 3/27/25
 * Desc: Defines and tracks the Notes of a Chord
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using ValuePitchEnums;

public class Chord : MonoBehaviour
{
    [Tooltip("The horizontal offset on change in Value, should be half of the distance between two Notes.")]
    public int horizontalOffset = 75;

    // Stores own Chord index in Song
    private int index;

    private Song song;

    private List<Note> notes;

    private Value value = Value.Quarter;

    private int focus = 0;

    // Start is called before the first frame update
    void Start()
    {
        song = GetComponentInParent<Song>();
        notes.Add(transform.GetChild(0).GetComponent<Note>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        while (value != Value.Quarter)
        {
            ChangeValue();
        }
    }

    // Used by Song to set index of Chord during Start
    public void SetIndex(int index)
    {
        this.index = index;
    }

    // Moves Note Pitch up by 1
    public void NoteUp(int pitch)
    {
        for (int i = pitch; i < )
        if ((int) pitch < 1)
        {
            image.sprite = chord.GetSprite((int)++pitch);
            transform.localPosition += new Vector3(0, verticalOffset);
        }
    }

    // Moves Note Pitch down by 1
    public void NoteDown(int pitch)
    {
        if (pitch > 0)
        {
            image.sprite = chord.GetSprite((int)--pitch);
            transform.localPosition -= new Vector3(0, verticalOffset);
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

        foreach (Note note in notes)
        {
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
                note.gameObject.SetActive(true);
            }
            else
            {
                note.gameObject.SetActive(false);
            }

            note.SetSprite(song.GetSprite((int) value, note.GetPitch()));
        }

        // Re-add outline
        notes[focus].Selected((int) value);

        // Size and position
        transform.localPosition += new Vector3((int) value / 2 * horizontalOffset, 0);
        transform.localScale *= (int) value;
    }

    public int GetValue()
    {
        return (int) value;
    }

    public void PlayNote()
    {

    }

    public Sprite GetSprite(int pitch)
    {
        return song.GetSprite((int) value, pitch);
    }

    public void SetFocus()
    {
        song.SetFocus(index);
    }
}
