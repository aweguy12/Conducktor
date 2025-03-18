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

public class Note : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Sprite[] sprites;
    public Song song;
    public float offset = 0.5f;
    private int note;
    [HideInInspector]
    public float value = 0f;
    private int index;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void NoteUp()
    {
        if (note < sprites.Length - 1)
        {
            image.sprite = sprites[++note];
            transform.position += new Vector3(0, offset);
        }
    }

    public void NoteDown()
    {
        if (note > 0)
        {
            image.sprite = sprites[--note];
            transform.position -= new Vector3(0, offset);
        }
    }

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
}
