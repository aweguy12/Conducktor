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
    [Tooltip("The vertical offset on change in Pitch.")]
    public int verticalOffset = 100;

    [Tooltip("The horizontal offset on change in Value, should be half of the distance between two Notes.")]
    public int horizontalOffset = 75;

    [Tooltip("Quarter note sprites in order, starting with rest.")]
    public Sprite[] quarterNoteSprites;

    [Tooltip("Half note sprites in order, starting with rest.")]
    public Sprite[] halfNoteSprites;

    [HideInInspector]
    public bool disabled = false;

    // Stores own Note index in Song
    private int index;
    
    // Remembers when Note is being dragged (used to prevent single-click Pitch changes after a drag)
    private bool dragged = false;

    private RectTransform rect;
    private Song song;
    private Image image;
    private Canvas canvas;
    private Value value = Value.Quarter;
    private Pitch pitch = Pitch.Rest;
    private Image[] outlines;
    private Sprite[][] sprites;

    // Start is called before the first frame update
    private void Start()
    {
        rect = transform.parent.GetComponent<RectTransform>();
        song = GetComponentInParent<Song>();
        image = GetComponent<Image>();
        canvas = transform.parent.parent.GetComponent<Canvas>();

        // Order Image components in children to match up with integral values of Value
        outlines = new Image[] { null, transform.GetChild(0).GetComponent<Image>(), transform.GetChild(1).GetComponent<Image>()};

        // Order these to match up with integral values of Value
        sprites = new Sprite[][] { null, quarterNoteSprites, halfNoteSprites };
    }

    // Resets Pitch to rest but preserves Value
    public void Reset()
    {
        while (pitch != Pitch.Rest)
        {
            NoteDown();
        }

        while (value != Value.Quarter)
        {
            ChangeValue();
        }

        image.sprite = sprites[(int) value][(int) pitch];
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
        outlines[(int) value].enabled = selected;
    }

    // Used by Song to set index of Note during Start
    public void SetIndex(int index)
    {
        this.index = index;
    }

    // Cycles between Values
    public void ChangeValue()
    {
        // Remove outline no matter what
        outlines[(int) value].enabled = false;

        // Reset size and position
        transform.localScale /= (int) value;
        transform.localPosition -= new Vector3((int) value / 2 * horizontalOffset, 0);

        switch (value)
        {
            case Value.Quarter:
                value = Value.Half;
                break;

            case Value.Half:
                value = Value.Quarter;
                break;
        }

        // Hides/reveals/modifies Note based on Value
        if (index % (int) value == 0)
        {
            image.enabled = true;
        }
        else
        {
            image.enabled = false;
        }

        // Resize and reposition
        transform.localPosition += new Vector3((int) value / 2 * horizontalOffset, 0);
        transform.localScale *= (int) value;

        image.sprite = sprites[(int) value][(int) pitch];
    }

    // Moves Note Pitch up by 1
    public void NoteUp()
    {
        if ((int) pitch < sprites[(int) value].Length - 1)
        {
            image.sprite = sprites[(int) value][(int) ++pitch];
            transform.localPosition += new Vector3(0, verticalOffset);
        }
    }

    // Moves Note Pitch down by 1
    public void NoteDown()
    {
        if (pitch > 0)
        {
            image.sprite = sprites[(int) value][(int) --pitch];
            transform.localPosition -= new Vector3(0, verticalOffset);
        }
    }

    // Set focus on click start
    public void OnPointerDown(PointerEventData eventData)
    {
        if (disabled)
        {
            return;
        }

        song.SetFocus(index);
        dragged = false;
    }

    // Moves Note on click end if Note was not dragged
    public void OnPointerUp(PointerEventData eventData)
    {
        if (disabled)
        {
            return;
        }

        if (!dragged)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    NoteUp();
                    break;
                case PointerEventData.InputButton.Right:
                    NoteDown();
                    break;
            }

            PlayCurrentNote();
        }
    }

    // Moves Note on drag
    public void OnDrag(PointerEventData eventData)
    {
        if (disabled)
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        if (localPoint.y < transform.localPosition.y - verticalOffset / 2)
        {
            NoteDown();
            dragged = true;
        }
        else if (localPoint.y * canvas.scaleFactor > transform.localPosition.y + verticalOffset / 2)
        {
            NoteUp();
            dragged = true;
        }
    }

    // Only plays Note sound after the drag
    public void OnEndDrag(PointerEventData eventData)
    {
        if (disabled)
        {
            return;
        }

        if (dragged)
        {
            PlayCurrentNote();
        }
    }
}
