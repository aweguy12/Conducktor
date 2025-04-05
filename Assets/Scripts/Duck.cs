using UnityEngine;
using UnityEngine.EventSystems;

public class Duck : MonoBehaviour, IPointerDownHandler
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

    public void ReplaySong()
    {
        animator.SetBool("Song", true);
    }

    public void SongStartEnd()
    {
        song.ReplaySong();
    }

    public void OnPointerDown(PointerEventData eventData)
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

            animator.SetBool("Quack", true);
            quacking = true;
        }
    }

    public void QuackEnd()
    {
        animator.SetBool("Quack", false);
        quacking = false;
    }

    public void FrameEnd()
    {
        song.FrameEnd();
    }
}
