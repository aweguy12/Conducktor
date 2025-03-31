/*
 * Name: Jack Gu, Danny Rosemond
 * Date: 3/28/25
 * Desc: Keeps track and controls the Notes of a song
 */

using System;
using UnityEngine;
using ValuePitchEnums;

public class Song : MonoBehaviour
{
    [Tooltip("Tempo of the song.")]
    public int tempo;

    [Tooltip("Individual chords in order.")]
    public Chord[] chords;

    [Tooltip("Quarter note sprites in order, starting with rest.")]
    public Sprite[] quarterNoteSprites;

    [Tooltip("Half note sprites in order, starting with rest.")]
    public Sprite[] halfNoteSprites;

    // Above Sprites in one array
    private Sprite[][] sprites;

    [Tooltip("Quarter note audio clips in order, start with an empty clip for rest.")]
    public AudioClip[] quarterNoteAudioClips;

    [Tooltip("Half note audio clips in order, start with an empty clip for rest.")]
    public AudioClip[] halfNoteAudioClips;

    // Above Audio Clips put in one array
    private AudioClip[][] audioClips;

    [Tooltip("Song for level 1, include empty notes after half notes.")]
    public NoteValuePitch[] level1;
    [Tooltip("Song for level 2, include empty notes after half notes.")]
    public NoteValuePitch[] level2;
    [Tooltip("Song for level 3, include empty notes after half notes.")]
    public NoteValuePitch[] level3;
    [Tooltip("Song for level 4, include empty notes after half notes.")]
    public NoteValuePitch[] level4;
    [Tooltip("Song for level 5, include empty notes after half notes.")]
    public NoteValuePitch[] level5;
    [Tooltip("Song for level 6, include empty notes after half notes.")]
    public NoteValuePitch[] level6;
    [Tooltip("Song for level 7, include empty notes after half notes.")]
    public NoteValuePitch[] level7;
    [Tooltip("Song for level 8, include empty notes after half notes.")]
    public NoteValuePitch[] level8;
    [Tooltip("Song for level 9, include empty notes after half notes.")]
    public NoteValuePitch[] level9;
    [Tooltip("Song for level 10, include empty notes after half notes.")]
    public NoteValuePitch[] level10;
    [Tooltip("Song for level 11, include empty notes after half notes.")]
    public NoteValuePitch[] level11;
    [Tooltip("Song for level 12, include empty notes after half notes.")]
    public NoteValuePitch[] level12;

    // Above levels put in one array
    private NoteValuePitch[][] levels;

    // 0-indexed level so level + 1 is actual level number
    private int level = 0;

    // Note that Song is focused/selected on, only modify this with SetFocus()
    private int focus = 0;

    [Serializable]
    // Used to simplify Song creation in editor
    public class NoteValuePitch
    {
        public Value value;
        public Pitch pitch;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Order both of these to match up with integral values of Value
        sprites = new Sprite[][] { null, quarterNoteSprites, halfNoteSprites };
        audioClips = new AudioClip[][] { null, quarterNoteAudioClips, halfNoteAudioClips };

        levels = new NoteValuePitch[][] { level1, level2, level3, level4, level5, level6, level7, level8, level9, level10, level11, level12 };
        
        // Tell individual Chords what index they are
        for (int i = 0; i < chords.Length; i++)
        {
            chords[i].SetIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check left direction key input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int i = 0;

            // Seek left for enabled Chord
            while (focus - ++i >= 0)
            {
                if (chords[focus - i].gameObject.activeSelf)
                {
                    SetFocus(focus - i);
                    break;
                }
            }

            chords[focus].PlayChord(AudioSettings.dspTime);
        }

        // Check right direction key input
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            int i = 0;

            // Seek right for enabled Chord
            while (focus + ++i < chords.Length)
            {
                if (chords[focus + i].gameObject.activeSelf)
                {
                    SetFocus(focus + i);
                    break;
                }
            }

            chords[focus].PlayChord(AudioSettings.dspTime);
        }

        // Check up direction key input        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            chords[focus].NoteUp();
            chords[focus].PlayChord(AudioSettings.dspTime);
        }

        // Check down direction key input
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            chords[focus].NoteDown();
            chords[focus].PlayChord(AudioSettings.dspTime);
        }

        // Space changes Note Value
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeNoteValue();
            chords[focus].PlayChord(AudioSettings.dspTime);
        }

        // Enter plays created song
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlaySong();
        }

        // R replays level song
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReplaySong();
        }
    }

    public int GetTempo()
    {
        return tempo;
    }
    public AudioClip GetAudioClip(int value, int pitch)
    {
        return audioClips[value][pitch];
    }

    public Sprite GetSprite(int value, int pitch)
    {
        return sprites[value][pitch];
    }

    // Changes focus on which Note is being modified
    public void SetFocus(int focus)
    {
        chords[this.focus].DeSelected();
        this.focus = focus;
        chords[focus].Selected();
    }

    public void AddNote()
    {
        chords[focus].AddNote();
    }

    public void RemoveNote()
    {
        chords[focus].RemoveNote();
    }

    // Called when Change Note Value button is pressed
    public void ChangeNoteValue()
    {
        if (focus % 2 == 0)
        {
            chords[focus].ChangeValue();
            chords[focus + 1].ChangeValue();
        }
        else
        {
            chords[focus].ChangeValue();
            chords[focus - 1].ChangeValue();
        }

        while (!chords[focus].gameObject.activeSelf)
        {
            SetFocus(focus - 1);
        }

        // Set focus again because it was reset by ChangeValue()
        SetFocus(focus);
    }

    // Plays the whole song
    public void PlaySong()
    {
        double currentTime = AudioSettings.dspTime;
        bool correct = true;

        for (int i = 0; i < chords.Length; i += chords[i].GetValue())
        {
            if (correct && chords[i].GetPitch() != (int) Pitch.Rest && chords[i].GetValue() != (int) levels[level][i].value && chords[i].GetPitch() != (int) levels[level][i].pitch)
            {
                correct = false;
            }

            chords[i].PlayChord(currentTime + i * 60 / tempo);
        }

        if (correct)
        {
            foreach (Chord chord in chords)
            {
                chord.Reset();
            }

            level++;

            SetFocus(0);
        }
    }

    // Replays the goal song
    public void ReplaySong()
    {

    }
}
