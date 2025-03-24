/*
 * Name: Jack Gu, Danny Rosemond
 * Date: 3/24/25
 * Desc: Keeps track and controls the Notes of a song
 */

using System;
using UnityEngine;
using ValuePitchEnums;

public class Song : MonoBehaviour
{
    public float tempo;
    public Note[] notes;
    public AudioClip[] quarterNoteAudioClips;
    public AudioClip[] halfNoteAudioClips;
    private AudioClip[][] audioClips;
    private int focus = 0;
    private AudioSource[] audioSources;
    public NoteValuePitch[] song1;
    public Transform selected;

    [Serializable]
    public class NoteValuePitch
    {
        public Value value;
        public Pitch pitch;
    }


    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        audioClips = new AudioClip[][] { new AudioClip[0], quarterNoteAudioClips, halfNoteAudioClips };
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int i = 0;
            while (focus - ++i >= 0)
            {
                if (notes[focus - i].isEnabled())
                {
                    SetFocus(focus - i);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            int i = 0;
            while (focus + ++i < notes.Length)
            {
                if (notes[focus + i].isEnabled())
                {
                    SetFocus(focus + i);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            notes[focus].NoteUp();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            notes[focus].NoteDown();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeNoteValue();
        }
    }


    // Changes focus on which Note is being modified
    public void SetFocus(int focus)
    {
        notes[this.focus].DeSelected();
        this.focus = focus;
        notes[focus].Selected();
    }

    // Called when Change Note Value button is pressed
    public void ChangeNoteValue()
    {
        if (focus % 2 == 0)
        {
            notes[focus].ChangeValue();
            notes[focus + 1].ChangeValue();
        }
        else
        {
            notes[focus].ChangeValue();
            notes[focus - 1].ChangeValue();
        }

        while (!notes[focus].isEnabled())
        {
            SetFocus(focus - 1);
        }
    }

    public void PlayNote()
    {
        audioSources[focus].clip = audioClips[notes[focus].GetValue()][notes[focus].GetPitch()];
        audioSources[focus].Play();
    }

    public void ReplaySong()
    {
        double currentTime = AudioSettings.dspTime;

        for (int i = 0; i < audioSources.Length; i += (int) song1[i].value)
        {
            audioSources[i].clip = audioClips[(int) song1[i].value][(int) song1[i].pitch];
            audioSources[i].PlayScheduled(currentTime + i * 60 / tempo);
            audioSources[i].SetScheduledEndTime(currentTime + (i + (int) song1[i].value) * 60 / tempo);
        }
    }

    // Plays the whole song
    public void PlaySong()
    {
        double currentTime = AudioSettings.dspTime;
        bool correct = true;

        for (int i = 0; i < audioSources.Length; i += notes[i].GetValue())
        {
            if (correct && notes[i].GetValue() != (int) song1[i].value && notes[i].GetPitch() != (int) song1[i].pitch)
            {
                correct = false;
            }

            audioSources[i].clip = audioClips[notes[i].GetValue()][notes[i].GetPitch()];
            audioSources[i].PlayScheduled(currentTime + i * 60 / tempo);
            audioSources[i].SetScheduledEndTime(currentTime + (i + notes[i].GetValue()) * 60 / tempo);
        }

        if (correct)
        {
            foreach (Note note in notes)
            {
                note.ResetToRest();
            }
        }
    }
}
