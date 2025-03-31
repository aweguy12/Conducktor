/*
 * Name: Jack Gu
 * Date: 3/31/25
 * Desc: Button pressed to remove a Note from Chord
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class RemoveNote : MonoBehaviour, IPointerClickHandler
{
    Song song;

    // Start is called before the first frame update
    void Start()
    {
        song = GetComponentInParent<Song>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        song.RemoveNote();
    }
}
