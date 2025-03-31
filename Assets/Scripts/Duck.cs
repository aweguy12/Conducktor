using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Duck : MonoBehaviour
{
    public Song Song;
    AudioSource AudioSource;
    Animator animator;

    public AudioClip[] audioClips;
    public GameObject playButton;
    public GameObject easyDuck;
    public GameObject medDuck;
    public GameObject hardDuck;

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
        if (easyDuck != null)
        {
            animator.Play("QuackEasy", 0, 0f);
            AudioSource.Play(2);
        }
        else if (medDuck != null)
        {
            animator.Play("QuackMed", 0, 0f);
            AudioSource.Play(3);
        }
        else if (hardDuck != null)
        {
            animator.Play("QuackHard", 0, 0f);
            AudioSource.Play(1);
        }
        
        
        
    }

    public void OnMouseDown()
    {
        
        Quack();
    }

    public void Update()
    {
        Debug.Log(Song.level);
        if (Song.level >= 3)
        {
            easyDuck.SetActive(false);
            hardDuck.SetActive(false);
            medDuck.SetActive(true);
        }
        else if (Song.level >= 7)
        {
            easyDuck.SetActive(false);
            medDuck.SetActive(false);
            hardDuck.SetActive(true);
        }
        else if (Song.level <=3)
        {
            easyDuck.SetActive(true);
            medDuck.SetActive(false);
            hardDuck.SetActive(false);
        }

        
    }
}
