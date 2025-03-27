using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Duck : MonoBehaviour
{
    public Song song;
    AudioSource AudioSource;
    Animator animator;


    public GameObject playButton;

    public void Start()
    {
        animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }
    
    public void Repeat()
    {
        animator.Play("PlayEasy", 0, 0f);
    }

    public void Quack()
    {
        animator.Play("QuackEasy", 0, 0f);
        AudioSource.Play();
        
    }

    public void OnMouseDown()
    {
        Quack();
    }
}
