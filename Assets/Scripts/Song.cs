/*
 * Name: Jack Gu, Danny Rosemond
 * Date: 3/20/25
 * Desc: Keeps track and controls the Notes of a song
 */

using System.Collections;
using UnityEngine;

public class Song : MonoBehaviour
{
    public float tempo;
    public Note[] notes;
    public AudioClip[] sounds;
    private int focus = 0;

    // Start is called before the first frame update
    void Start()
    {
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
                    focus -= i;
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
                    focus += i;
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
            ChangeValue();
        }
    }


    // Changes focus on which Note is being modified
    public void SetFocus(int focus)
    {
        this.focus = focus;
    }

    // Called when Change Value button is pressed
    public void ChangeValue()
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
            focus--;
        }
    }

    // Plays the whole song
    public void PlaySong()
    {
        StartCoroutine(PlayNotes());
    }

    private IEnumerator PlayNotes()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            yield return null;
        }
    }
}
