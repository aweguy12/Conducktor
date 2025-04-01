using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DuckEasy : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public Song song;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Repeat()
    {
        Debug.Log("Repeat");
        animator.Play("PlayEasy", 0, 0f);
    }

    // Update is called once per frame
    public void Quack()
    {
        animator.Play("QuackEasy", 0, 0f);
        audioSource.Play();
    }

    public void Update()
    {
        if (song.level <= 3)
        {
            gameObject.SetActive(true);
        }
        else if (song.level > 3)
        {
            gameObject.SetActive(false);
        }
    }

}
