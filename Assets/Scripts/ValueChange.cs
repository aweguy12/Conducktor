/*
 * Name: Jack Gu
 * Date: 3/19/25
 * Desc: Tells the Song when the focused Note's value should be changed
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class ValueChange : MonoBehaviour, IPointerClickHandler
{
    public Song song;

    public void OnPointerClick(PointerEventData eventData)
    {
        song.ValueChange();
    }
}
