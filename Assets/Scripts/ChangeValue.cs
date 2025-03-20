/*
 * Name: Jack Gu
 * Date: 3/20/25
 * Desc: Tells the Song when the focused Note's value should be changed
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeValue : MonoBehaviour, IPointerClickHandler
{
    private Song song;

    private void Start()
    {
        song = GetComponentInParent<Song>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        song.ChangeValue();
    }
}
