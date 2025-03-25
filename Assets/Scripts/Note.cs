/*
 * Name: Jack Gu, Danny Rosemond
 * Date: 3/25/25
 * Desc: Defines and controls a single note
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ValuePitchEnums;

public class Note : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("Quarter note sprites in order, starting with rest.")]
    public Sprite[] quarterNoteSprites;

    [Tooltip("Half note sprites in order, starting with rest.")]
    public Sprite[] halfNoteSprites;

    [Tooltip("The vertical offset on change in Pitch.")]
    public float offset = 0.5f;

    // Stores own Note index in Song
    private int index;
    
    // Remembers when Note is being dragged (used to prevent single-click Pitch changes after a drag)
    private bool dragged = false;
    
    private Song song;
    private Image image;
    private Image selected;
    private Value value = Value.Quarter;
    private Pitch pitch = Pitch.Rest;
    private Sprite[][] sprites;

    // Start is called before the first frame update
    private void Start()
    {
        song = GetComponentInParent<Song>();
        image = GetComponent<Image>();
        selected = transform.GetChild(0).GetComponent<Image>();

        // Order these to match up with integral values of Value
        sprites = new Sprite[][] { new Sprite[0], quarterNoteSprites, halfNoteSprites };
    }

    // Resets Pitch to rest but preserves Value
    public void Reset()
    {
        transform.position -= new Vector3(0, offset * (int) pitch);
        pitch = Pitch.Rest;
        image.sprite = sprites[(int) value][(int) Pitch.Rest];
    }

    public int GetPitch()
    {
        return (int) pitch;
    }

    public int GetValue()
    {
        return (int) value;
    }

    public bool isEnabled()
    {
        return image.enabled;
    }

    public void PlayCurrentNote()
    {
        song.PlayNote();
    }

    // Used by Song to enable/disable focus on the Note
    public void Selected(bool selected)
    {
        this.selected.enabled = selected;
    }

    // Used by Song to set index of Note during Start
    public void SetIndex(int index)
    {
        this.index = index;
    }

    // Cycles between Values
    public void ChangeValue()
    {
        switch (value)
        {
            case Value.Quarter:
                value = Value.Half;
                break;

            case Value.Half:
                value = Value.Quarter;
                break;
        }

        // Hides/reveals Note based on Value
        if (index % (int) value == 0)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }

        image.sprite = sprites[(int) value][(int) pitch];
    }

    // Moves Note Pitch up by 1
    public void NoteUp()
    {
        if ((int) pitch < sprites[(int) value].Length - 1)
        {
            image.sprite = sprites[(int) value][(int) ++pitch];
            transform.position += new Vector3(0, offset);
        }
    }

    // Moves Note Pitch down by 1
    public void NoteDown()
    {
        if (pitch > 0)
        {
            image.sprite = sprites[(int) value][(int) --pitch];
            transform.position -= new Vector3(0, offset);
        }
    }

    // Set focus on click start
    public void OnPointerDown(PointerEventData eventData)
    {
        song.SetFocus(index);
        dragged = false;
    }

    // Moves Note on click end if Note was not dragged
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!dragged)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    NoteDown();
                    break;
                case PointerEventData.InputButton.Right:
                    NoteUp();
                    break;
            }

            PlayCurrentNote();
        }
    }

    // Moves Note on drag
    public void OnDrag(PointerEventData eventData)
    {
        if (Camera.main.ScreenToWorldPoint(eventData.position).y < transform.position.y - offset / 2)
        {
            NoteDown();
            dragged = true;
        }
        else if (Camera.main.ScreenToWorldPoint(eventData.position).y > transform.position.y + offset / 2)
        {
            NoteUp();
            dragged = true;
        }
    }

    // Only plays Note sound after the drag
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragged)
        {
            PlayCurrentNote();
        }
    }
}
