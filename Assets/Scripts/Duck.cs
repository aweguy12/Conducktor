using UnityEngine;
using UnityEngine.EventSystems;

public class Duck : MonoBehaviour, IPointerDownHandler
{
    public AudioClip[] quacks;

    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.clip = quacks[Random.Range(0, quacks.Length)];
        animator.SetBool("Quack", true);
    }

    public void QuackEnd()
    {
        animator.SetBool("Quack", false);
    }
}
