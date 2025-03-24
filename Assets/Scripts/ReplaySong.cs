/*
 * Name: Jack Gu
 * Date: 3/20/25
 * Desc: Tells the Song to replay the given song
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class ReplaySong : MonoBehaviour, IPointerClickHandler
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
        song.ReplaySong();
    }
}
