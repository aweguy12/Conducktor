/*
 * Name: Jack Gu
 * Date: 4/3/25
 * Desc: Animates object on mouse hover
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class AnimateOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.enabled = false;
    }
}
