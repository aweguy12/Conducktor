/*
 * Name: Jack Gu
 * Date: 4/10/25
 * Desc: Controls certain duck functions
 */

using UnityEngine;

public class Duck : MonoBehaviour
{
    [Tooltip("Quack on click variants, randomized.")]
    public AudioClip[] quacks;

    [Tooltip("Robot duck quack.")]
    public AudioClip robotQuack;

    public Song song;

    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SongStart()
    {
        song.ReplaySong();
    }

    public void Clicked()
    {
        if (animator.GetInteger("Difficulty") == 2)
        {
            Quack(robotQuack);
        }
        else
        {
            Quack(quacks[Random.Range(0, quacks.Length)]);
        }
    }

    public void Quack(AudioClip clip)
    {
        animator.SetTrigger("Quack");
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void SetDifficulty(int difficulty)
    {
        animator.SetInteger("Difficulty", difficulty);
    }

    public void SetValue(int value)
    {
        animator.SetInteger("Value", value);
    }
}
