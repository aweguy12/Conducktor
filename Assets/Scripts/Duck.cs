using UnityEngine;
using UnityEngine.EventSystems;

public class Duck : MonoBehaviour
{
    public AudioClip[] quacks;
    public AudioClip robotQuack;
    public Song song;

    private Animator animator;
    private AudioSource audioSource;
    private bool quacking = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SongStartEnd()
    {
        song.ReplaySong();
    }

    public void Quack()
    {
        if (!quacking)
        {
            if (animator.GetInteger("Difficulty") == 2)
            {
                audioSource.clip = robotQuack;
            }
            else
            {
                audioSource.clip = quacks[Random.Range(0, quacks.Length)];
            }

            animator.SetTrigger("Quack");
            quacking = true;
        }
    }

    public void QuackEnd()
    {
        quacking = false;
    }
}
