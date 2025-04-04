using UnityEngine;
using UnityEngine.EventSystems;

public class Duck : MonoBehaviour, IPointerDownHandler
{
    public AudioClip[] quacks;

    private Animator animator;
    private AudioSource audioSource;
    private bool quacking = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!quacking)
        {
            audioSource.clip = quacks[Random.Range(0, quacks.Length)];
            animator.SetBool("Quack", true);
            quacking = true;
        }
    }

    public void QuackEnd()
    {
        animator.SetBool("Quack", false);
        quacking = false;
    }
}
