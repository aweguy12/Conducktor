/*
 * Name: Jack Gu
 * Date: 3/21/25
 * Desc: Tells the Song to play the player-created song
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySong : MonoBehaviour, IPointerClickHandler
{
    private Song song;

    private void Start()
    {
        song = transform.parent.GetComponent<Song>();
    }

    // Called when GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        song.PlaySong();
    }
}
