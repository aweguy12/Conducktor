/*
 * Name: Jack Gu, Danny Rosemond
 * Date: 3/24/25
 * Desc: Keeps track and controls the Notes of a song
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ValuePitchEnums;

public class Song : MonoBehaviour
{
    public InteractionHandler interactionHandler;
    public Image changeValue;
    public Sprite[] values;
    public Duck duck;
    public UnityEvent win;
    private int note = 0;
    public Animator duckAnimator;
    public AudioSource duckAudioSource;
    public AudioClip[] winSounds;
    public AudioClip[] loseSounds;
    public float waitTime = 1.5f;

    [HideInInspector]
    public bool disabled = true;

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
        audioSources = GetComponentsInChildren<AudioSource>();

        // Ordered to match integral values of Value
        audioClips = new AudioClip[][] { null, quarterNoteAudioClips, halfNoteAudioClips };
        levels = new NoteValuePitch[][] { level1, level2, level3, level4, level5, level6, level7, level8, level9 };
        
        // Tell individual Notes what index they are
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].SetIndex(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (disabled)
        {
            return;
        }

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
        }

        // Enter plays created song
        if (Input.GetKeyDown(KeyCode.Return))
        {
            interactionHandler.Buttons(false);
            interactionHandler.SongNotes(true);
            PlaySong();
        }

        // R replays level song
        if (Input.GetKeyDown(KeyCode.R))
        {
            interactionHandler.Buttons(false);
            interactionHandler.SongNotes(true);
            duckAnimator.SetTrigger("Replay");
        }
    }


    // Changes focus on which Note is being modified
    public void SetFocus(int focus)
    {
        notes[this.focus].Selected(false);
        this.focus = focus;
        notes[focus].Selected(true);
        changeValue.sprite = values[notes[focus].GetValue()];
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

        PlayNote();
    }

    // Plays currently selected Note on modification
    public void PlayNote()
    {
        audioSources[focus].clip = audioClips[notes[focus].GetValue()][notes[focus].GetPitch()];
        audioSources[focus].Play();
    }

    // Plays the whole song
    public void PlaySong()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        bool correct = true;
        int prefocus = focus;

        for (int i = 0; i < notes.Length; i += notes[i].GetValue())
        {
            if (correct && (notes[i].GetValue() != (int) levels[level][i].value || notes[i].GetPitch() != (int) levels[level][i].pitch))
            {
                correct = false;
            }

            SetFocus(i);
            audioSources[i].clip = audioClips[notes[i].GetValue()][notes[i].GetPitch()];
            audioSources[i].Play();
            yield return new WaitForSeconds((float) notes[i].GetValue() * 60 / tempo);
        }

        yield return new WaitForSeconds(waitTime);

        SetFocus(prefocus);

        if (correct)
        {
            foreach (Note note in notes)
            {
                note.Reset();
            }

            duck.Quack(winSounds[UnityEngine.Random.Range(0, winSounds.Length)]);

            if (++level == 9)
            {
                yield return new WaitForSeconds(waitTime);
                level = 0;
                duck.SetDifficulty(0);
                win.Invoke();
            }
            else
            {
                duck.SetDifficulty(level / 3);
                interactionHandler.Buttons(true);
                interactionHandler.SongNotes(false);
            }

            SetFocus(0);
        }
        else
        {
            duck.Quack(loseSounds[UnityEngine.Random.Range(0, loseSounds.Length)]);
            interactionHandler.Buttons(true);
            interactionHandler.SongNotes(false);
        }
    }

    // Replays the goal song
    public void ReplaySong()
    {
        StartCoroutine(Replay());  
    }

    IEnumerator Replay()
    {
        note = 0;
        for (int i = 0; i < audioSources.Length; i += (int) levels[level][i].value)
        {
            audioSources[i].clip = audioClips[(int) levels[level][i].value][(int) levels[level][i].pitch];
            audioSources[i].Play();
            duck.SetValue(levels[level][note].pitch == Pitch.Rest ? 0 : (int) levels[level][note].value);
            yield return new WaitForSeconds((float) levels[level][note].value * 60 / tempo);
            note += (int) levels[level][note].value;

            if (note >= 8)
            {
                duckAnimator.SetInteger("Value", 3);
                interactionHandler.Buttons(true);
                interactionHandler.SongNotes(false);
                yield break;
            }
        }
    }
}
