/*
 * Name: Jack Gu, Danny Rosemond
 * Date: 3/20/25
 * Desc: Defines and controls a single note
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class Note : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Sprite[] quarterNoteSprites;
    public Sprite[] halfNoteSprites;
    private Sprite[][] sprites;
    private Song song;
    public float offset = 0.5f;
    private int index;
    private Image image;
    private Pitch pitch = Pitch.Rest;
    private Value value = Value.Quarter;

    public enum Value
    {
        Quarter, Half
    }

    public enum Pitch
    {
        Rest, C4, D4, E4, F4, G4, A4, B4, C5
    }

    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponent<Image>();
        song = GetComponentInParent<Song>();
        sprites = new Sprite[][] { quarterNoteSprites, halfNoteSprites };
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sets index of note at Start
    public void SetIndex(int index)
    {
        this.index = index;
    }

    public bool isEnabled()
    {
        return image.enabled;
    }

    public void ChangeValue()
    {
        switch (value)
        {
            case Value.Quarter:
                value = Value.Half;
                
                if (index % 2 == 0)
                {
                    image.sprite = sprites[(int) value][(int) pitch];
                }
                else
                {
                    image.enabled = false;
                }
                break;
            case Value.Half:
                value = Value.Quarter;
                
                if (index % 2 == 0)
                {
                    image.sprite = sprites[(int) value][(int) pitch];
                }
                else
                {
                    image.enabled = true;
                }
                break;
        }
    }

    // Moves note pitch up
    public void NoteUp()
    {
        if ((int) pitch < sprites[(int) value].Length - 1)
        {
            image.sprite = sprites[(int) value][(int) ++pitch];
            transform.position += new Vector3(0, offset);
        }
    }

    // Moves note pitch down
    public void NoteDown()
    {
        if (pitch > 0)
        {
            image.sprite = sprites[(int) value][(int) --pitch];
            transform.position -= new Vector3(0, offset);
        }
    }

    // Moves note on click
    public void OnPointerClick(PointerEventData eventData)
    {
        song.SetFocus(index);
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            NoteDown();   
        }
        else
        {
            NoteUp();
        }
    }

    // Moves note on drag
    public void OnDrag(PointerEventData eventData)
    {
        if (Camera.main.ScreenToWorldPoint(eventData.position).y < transform.position.y - offset / 2)
        {
            NoteDown();
        }
        else if (Camera.main.ScreenToWorldPoint(eventData.position).y > transform.position.y + offset / 2)
        {
            NoteUp();
        }
    }

    public void ResetToRest()
    {
        transform.position -= new Vector3(0, offset * (int) pitch);
        pitch = Pitch.Rest;
        image.sprite = sprites[(int) value][0];
    }
}
