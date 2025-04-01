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
    [Tooltip("Tempo of the song.")]
    public float tempo;

    [Tooltip("Individual notes in order.")]
    public Note[] notes;

    [Tooltip("Quarter note audio clips in order, start with an empty clip for rest.")]
    public AudioClip[] quarterNoteAudioClips;

    [Tooltip("Half note audio clips in order, start with an empty clip for rest.")]
    public AudioClip[] halfNoteAudioClips;

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
    
    // 0-indexed level so level + 1 is actual level number
    public int level = 0;

    // Note that Song is focused/selected on, only modify this with SetFocus()
    private int focus = 0;

    // Note Audio Sources in order
    private AudioSource[] audioSources;

    // Above Audio Clips put in one array
    private AudioClip[][] audioClips;

    // Above levels put in one array
    private NoteValuePitch[][] levels;

    //level-up sprites
    public GameObject levelSprite;
    Animator anim;
    public FadeOut fadeOut;

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
        fadeOut = GetComponent<FadeOut>();
        anim = GetComponent<Animator>();
        audioSources = GetComponentsInChildren<AudioSource>();

        // Ordered to match integral values of Value
        audioClips = new AudioClip[][] { null, quarterNoteAudioClips, halfNoteAudioClips };
        levels = new NoteValuePitch[][] { level1, level2, level3, level4, level5, level6, level7, level8, level9, level10, level11, level12 };
        
        // Tell individual Notes what index they are
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check left direction key input
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int i = 0;
            // Seek left for enabled Note
            while (focus - ++i >= 0)
            {
                if (notes[focus - i].isEnabled())
                {
                    SetFocus(focus - i);
                    break;
                }
            }

            PlayNote();
        }

        // Check right direction key input
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            int i = 0;

            // Seek right for enabled Note
            while (focus + ++i < notes.Length)
            {
                if (notes[focus + i].isEnabled())
                {
                    SetFocus(focus + i);
                    break;
                }
            }

            PlayNote();
        }

        // Check up direction key input        
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            notes[focus].NoteUp();
            PlayNote();
        }

        // Check down direction key input
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            notes[focus].NoteDown();
            PlayNote();
        }

        // Space changes Note Value
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeNoteValue();
            PlayNote();
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


    // Changes focus on which Note is being modified
    public void SetFocus(int focus)
    {
        notes[this.focus].Selected(false);
        this.focus = focus;
        notes[focus].Selected(true);
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

        // Set focus again because it was reset by ChangeValue()
        SetFocus(focus);
    }

    // Plays currently selected Note on modification
    public void PlayNote()
    {
        audioSources[focus].clip = audioClips[notes[focus].GetValue()][notes[focus].GetPitch()];
        audioSources[focus].Play();
        audioSources[focus].SetScheduledEndTime(AudioSettings.dspTime + notes[focus].GetValue() * 60 / tempo);
    }

    // Plays the whole song
    public void PlaySong()
    {
        double currentTime = AudioSettings.dspTime;
        bool correct = true;

        for (int i = 0; i < audioSources.Length; i += notes[i].GetValue())
        {
            if (correct && notes[i].GetPitch() != (int) Pitch.Rest && notes[i].GetValue() != (int) levels[level][i].value && notes[i].GetPitch() != (int) levels[level][i].pitch)
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
                note.Reset();
            }

            level++;
            levelSprite.SetActive(true);

            SetFocus(0);
        }
    }

    // Replays the goal song
    public void ReplaySong()
    {
        double currentTime = AudioSettings.dspTime;

        for (int i = 0; i < audioSources.Length; i += (int) levels[level][i].value)
        {
            audioSources[i].clip = audioClips[(int) levels[level][i].value][(int) levels[level][i].pitch];
            audioSources[i].PlayScheduled(currentTime + i * 60 / tempo);
            audioSources[i].SetScheduledEndTime(currentTime + (i + (int) levels[level][i].value) * 60 / tempo);
        }
    }

   
}
