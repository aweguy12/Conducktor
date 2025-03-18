using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ValueChange : MonoBehaviour, IPointerClickHandler
{
    public Song song;

    public void OnPointerClick(PointerEventData eventData)
    {
        song.ValueChange();
    }
}
