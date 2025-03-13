/*
 * Name: Jack Gu
 * Date: 3/13/25
 * Desc: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public Sprite[] notes;
    public AudioSource[] tones;
    private int note;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && note < notes.Length - 1)
        {
            sr.sprite = notes[++note];
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && note > 0)
        {
            sr.sprite = notes[--note];
        }
    }

    public void playNote()
    {
        tones[note].Play();
    }

    public float getClipLength()
    {
        return tones[note].clip.length;
    }
}
