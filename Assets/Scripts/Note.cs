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
    
    // Remembers when Note is being dragged (used to prevent single-click Pitch changes after a drag)
    private bool dragged = false;


    private RectTransform rect;
    private Chord chord;
    private Image image;
    private Canvas canvas;
    private Pitch pitch = Pitch.Rest;
    private Image[] outlines;

    // Start is called before the first frame update
    private void Start()
    {
        rect = transform.parent.parent.parent.GetComponent<RectTransform>();
        chord = GetComponentInParent<Chord>();
        image = GetComponent<Image>();
        canvas = rect.GetComponent<Canvas>();

        // Order Image components in children to match up with integral values of Value
        outlines = new Image[] { null, transform.GetChild(0).GetComponent<Image>(), transform.GetChild(1).GetComponent<Image>()};
    }

    // Resets Pitch to rest but preserves Value
    public void Reset()
    {
        while (pitch != Pitch.Rest)
        {
            chord.NoteDown((int) pitch);
        }

        image.sprite = chord.GetSprite((int) pitch);
    }

    public int GetPitch()
    {
        return (int) pitch;
    }

    public bool isEnabled()
    {
        return image.enabled;
    }

    public void PlayCurrentNote()
    {
        chord.PlayNote();
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    // Used by Song -> Chord to enable focus on the Note
    public void Selected(int value)
    {
        outlines[value].enabled = true;
    }


    // Used by Song -> Chord to disable focus on the Note
    public void DeSelected(int value)
    {
        outlines[value].enabled = false;
    }

    // Set focus on click start
    public void OnPointerDown(PointerEventData eventData)
    {
        chord.SetFocus();
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
                    chord.NoteDown((int) pitch);
                    break;
                case PointerEventData.InputButton.Right:
                    chord.NoteUp((int) pitch);
                    break;
            }

            PlayCurrentNote();
        }
    }

    // Moves Note on drag
    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        if (localPoint.y < transform.localPosition.y - verticalOffset / 2)
        {
            chord.NoteDown((int) pitch);
            dragged = true;
        }
        else if (localPoint.y * canvas.scaleFactor > transform.localPosition.y + verticalOffset / 2)
        {
            chord.NoteUp((int) pitch);
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
