/*
 * Name: Jack Gu
 * Date: 3/20/25
 * Desc: Tells the Song to replay the given song
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class ReplaySong : MonoBehaviour, IPointerClickHandler
{
    public Duck duck;

    // Called when GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        duck.ReplaySong();
    }
}
