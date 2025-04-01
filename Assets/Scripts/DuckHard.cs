using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHard : MonoBehaviour
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
        animator.Play("PlayHard", 0, 0f);
    }

    // Update is called once per frame
    public void Quack()
    {
        animator.Play("QuackHard", 0, 0f);
        audioSource.Play();
    }
    public void Update()
    {
        if (song.level >= 6)
        {
            gameObject.SetActive(true);
        }
        else if (song.level < 6)
        {
            gameObject.SetActive(false);
        }
    }
}
