/*
 * Name: Jack Gu, Danny Rosemond
 * Date: 3/28/25
 * Desc: Defines and controls a single note
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ValuePitchEnums;

public class Note : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("The vertical offset on change in Pitch.")]
    public int pitchOffset = 100;

    [Tooltip("The horizontal offset for displaying adjacent Notes in a Chord.")]
    public int adjacentOffset = 50;

    // Remembers when Note is being dragged (used to prevent single-click Pitch changes after a drag)
    private bool dragged = false;

    private bool offset = false;
    private RectTransform rect;
    private Canvas canvas;
    private Chord chord;
    private Image image;
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
            NoteDown();
        }

        image.sprite = chord.GetSprite((int) pitch);
    }

    public int GetPitch()
    {
        return (int) pitch;
    }

    public bool IsEnabled()
    {
        return image.enabled;
    }

    public void Offset(bool off)
    {
        if (off)
        {
            transform.localPosition += new Vector3(adjacentOffset, 0);
            offset = true;
        }
        else if (offset)
        {
            transform.localPosition -= new Vector3(adjacentOffset, 0);
            offset = false;
        }
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

    // Moves Note Pitch up by 1
    public void NoteUp()
    {
        image.sprite = chord.GetSprite((int)++pitch);
        transform.localPosition += new Vector3(0, pitchOffset);
    }

    // Moves Note Pitch down by 1
    public void NoteDown()
    {
        image.sprite = chord.GetSprite((int) --pitch);
        transform.localPosition -= new Vector3(0, pitchOffset);
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
                    chord.NoteDown();
                    break;
                case PointerEventData.InputButton.Right:
                    chord.NoteUp();
                    break;
            }

            PlayCurrentNote();
        }
    }

    // Moves Note on drag
    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        if (localPoint.y < transform.localPosition.y - pitchOffset / 2)
        {
            chord.NoteDown();
            dragged = true;
        }
        else if (localPoint.y * canvas.scaleFactor > transform.localPosition.y + pitchOffset / 2)
        {
            chord.NoteUp();
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
