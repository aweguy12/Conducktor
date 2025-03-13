/*
 * Name: Jack Gu
 * Date: 3/13/25
 * Desc: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public Note[] notes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSong()
    {
        StartCoroutine(playNotes());
    }

    IEnumerator playNotes()
    {
        for (int i = 0; i < notes.Length; i++)
        {
            notes[i].playNote();
            yield return new WaitForSeconds(notes[i].getClipLength());
        }
    }
}
