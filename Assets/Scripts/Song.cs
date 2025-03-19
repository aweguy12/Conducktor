/*
 * Name: Jack Gu
 * Date: 3/13/25
 * Desc: Keeps track and controls the Notes of a song
 */

using System.Collections;
using UnityEngine;

public class Song : MonoBehaviour
{
    public float tempo;
    public Note[] notes;
    public AudioSource[] sounds;
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
            if (focus > 0)
            {
                focus--;
            }
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (focus < notes.Length - 1)
            {
                focus++;
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
    }


    // Changes focus on which Note is being modified
    public void SetFocus(int focus)
    {
        this.focus = focus;
    }

    // Called when Value Change button is pressed
    public void ValueChange()
    {
        
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
