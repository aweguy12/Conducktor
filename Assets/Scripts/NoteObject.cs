/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteObject : MonoBehaviour
{
    public NoteSelector noteSelector;
    public int noteIndex;


    public Text noteText;
    public Image selectionHighlight;

    private bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        selectionHighlight.enabled = false;
    }

    public void SetNote(NoteSelector.Note note)
    {
        noteText.text = note.ToString();
    }

    public void SelectNote()
    {
        isSelected = true;
        selectionHighlight.enabled = true;
        noteSelector.SelectNote(this);
        Debug.Log("NoteObject " + noteIndex + " selected.");
    }

    public void DeselectNote()
    {
        isSelected = false;
        selectionHighlight.enabled = false;
        Debug.Log("NoteObject " + noteIndex + "deselected.");
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void OnMouseDown()
    {
        Debug.Log("NoteObject " + noteIndex + "clicked.");

        noteSelector.SelectNote(this);

        if (noteSelector.selectedNote != null && noteSelector.selectedNote != this)
        {
            SetNote(noteSelector.GetNote(noteSelector.selectedNote));
            Debug.Log("NoteObject " + noteIndex + " changed to " + noteSelector.GetNote(noteSelector.selectedNote));
        }

    }

    // Update is called once per frame
    public bool IsCurrentlySelected()
    {
        return noteSelector.selectedNote == this;
    }

}
*/