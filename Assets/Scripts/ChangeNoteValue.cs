/*
 * Name: Jack Gu
 * Date: 3/20/25
 * Desc: Tells the Song when the focused Note's value should be changed
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeNoteValue : MonoBehaviour, IPointerClickHandler
{
    private Song song;

    // Start is called before the first frame update
    private void Start()
    {
        song = GetComponentInParent<Song>();
    }

    // Called when GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        song.ChangeNoteValue();
    }
}
