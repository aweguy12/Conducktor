using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Duck : MonoBehaviour
{
    AudioSource AudioSource;
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
        
    }
    public void OnMouseDown()
    {
        GetComponent<Animator>().Play("QuackEasy", -1, 0f);
        AudioSource.Play();
        Debug.Log("Quack");

    }
}
