/*
 * Name: Jack Gu
 * Date: 4/6/25
 * Desc: Enables/disables a Song and Notes
 */

using UnityEngine;

public class SongNoteEnabler : MonoBehaviour
{
    public Song song;
    public Note[] notes;

    public void EnDisable(bool enabled)
    {
        song.enabled = enabled;

        foreach (Note note in notes)
        {
            note.enabled = enabled;
        }
    }
}
