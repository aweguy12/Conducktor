/*
 * Name: Jack Gu
 * Date: 4/6/25
 * Desc: Enables/disables Song/Notes and/or multiple buttons simultaneously
 */

using UnityEngine;
using UnityEngine.UI;

public class InteractionHandler : MonoBehaviour
{
    [Tooltip("Buttons that are to be enabled/disabled.")]
    public Button[] buttons;

    [Tooltip("Song that is to be enabled/disabled.")]
    public Song song;

    [Tooltip("Notes that are to be enabled/disabled.")]
    public Note[] notes;

    // Enables/disables buttons
    public void Buttons(bool interactable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = interactable;
        }
    }

    // Enables/disables Song and Notes
    public void SongNotes(bool disabled)
    {
        song.disabled = disabled;

        foreach (Note note in notes)
        {
            note.disabled = disabled;
        }
    }
}
