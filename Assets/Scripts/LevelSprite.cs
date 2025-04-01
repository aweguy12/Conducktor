using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSprite : MonoBehaviour
{
    public Song song;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (song.level == 1)
        {
            anim.Play("Level 1");
            Debug.Log("Level 1");

        }
        else if (song.level == 2)
        {
            anim.Play("Level 2");
            Debug.Log("Level 2");

        }
        else if (song.level == 3)
        {
            anim.Play("Level 3");
            Debug.Log("Level 3");

        }
        else if (song.level == 4)
        {
            anim.Play("Level 4");
            Debug.Log("Level 4");

        }
        else if (song.level == 5)
        {
            anim.Play("Level 5");
            Debug.Log("Level 5");
            ;
        }
        else if (song.level == 6)
        {
            anim.Play("Level 6");
            Debug.Log("Level 6");

        }
        else if (song.level == 7)
        {
            anim.Play("level 7");
            Debug.Log("Level 7");

        }
        else if (song.level == 8)
        {
            anim.Play("level 8");
            Debug.Log("Level 8");

        }
        else if (song.level == 9)
        {
            anim.Play("level 9");
            Debug.Log("Level 9");

        }
        else if (song.level == 10)
        {
            anim.Play("level 10");
            Debug.Log("Level 10");

        }
        else if (song.level == 11)
        {
            anim.Play("level 11");
            Debug.Log("Level 11");

        }
        else if (song.level == 12)
        {
            anim.Play("level 12");
            Debug.Log("Level 12");

        }
    }
}
