/*
 * Name: Jack Gu
 * Date: 3/13/25
 * Desc: Defines and controls a single note
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Note : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Sprite[][] sprites;
    public Sprite restSprite;
    public Sprite[] quarterNoteSprites;
    public Sprite[] halfNoteSprites;
    public Song song;
    public float offset = 0.5f;
    private int note;
    [HideInInspector]
    private int index;
    private Image image;
    private Pitch pitch;
    private Value value;
    private Vector3 initialPosition;

    public enum Value
    {
        Rest, Quarter, Half
    }

    public enum Pitch
    {
        C4, D4, E4, F4, G4, A4, B4, C5
    }

    // Start is called before the first frame update
    private void Start()
    {
        image = GetComponent<Image>();
        initialPosition = transform.position;

    // Update is called once per frame
    void Update()
    {

    }

    // Sets index of note at Start
    public void SetIndex(int index)
    {
        this.index = index;
    }

    // Moves note pitch up
    public void NoteUp()
    {
        if ((int) pitch < Enum.GetNames(typeof(Pitch)).Length - 1)
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
        value = Value.Rest;
        pitch = Pitch.C4;
        image.sprite = sprites[0][0];
        transform.position = initialPosition;
    }
}
