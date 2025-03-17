/*
 * Name: Jack Gu
 * Date: 3/13/25
 * Desc: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Note : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] notes;
    public AudioSource[] tones;
    private int note;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && note < notes.Length - 1)
        {
            image.sprite = notes[++note];
            transform.position += new Vector3(0, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && note > 0)
        {
            image.sprite = notes[--note];
            transform.position -= new Vector3(0, 0.5f);
        }
    }

    public void playNote()
    {
        tones[note].Play();
    }

    public void stopNote()
    {
        tones[note].Stop();
    }

    public float getClipLength()
    {
        return tones[note].clip.length;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (note < notes.Length - 1)
        {
            image.sprite = notes[++note];
            transform.position += new Vector3(0, 0.5f);
        }
    }
}
