/*
 * Name: Jack Gu
 * Date: 3/21/25
 * Desc: Tells the Song when the player created song should be played
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySong : MonoBehaviour, IPointerClickHandler
{
    private Song song;

    // Start is called before the first frame update
    void Start()
    {
        song = GetComponentInParent<Song>();
    }

    // Called when GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        song.PlaySong();
    }
}
