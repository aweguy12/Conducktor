/*
 * Name: Jack Gu
 * Date: 4/6/25
 * Desc: Enables/disables multiple buttons simultaneously
 */

using UnityEngine;
using UnityEngine.UI;

public class InteractableButtons : MonoBehaviour
{
    public Button[] buttons;

    public void Interactable(bool interactable)
    {
        foreach (Button button in buttons)
        {
            button.interactable = interactable;
        }
    }
}
