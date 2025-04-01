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
    //added by danny
    public DuckEasy duckEasy;
    public DuckMed duckMed;
    public DuckHard duckHard;

    // Start is called before the first frame update
    private void Start()
    {
        song = GetComponentInParent<Song>();
    }

    // Called when GameObject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        song.ReplaySong();
        //added by danny
        duckEasy.Repeat();
        duckMed.Repeat();
        duckHard.Repeat();
    }
}
