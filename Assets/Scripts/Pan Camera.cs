/*
 * Name: Jack Gu
 * Date: 4/1/25
 * Desc: Pans the camera with speed up and slow down
 */

using UnityEngine;

public class PanCamera : MonoBehaviour
{
    public bool down = false;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (down)
        {
            if (rb.velocity.y >= 0.01)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }
        else
        {
            if (rb.velocity.y <= -0.01)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            }
        }
    }

    // Pans camera to given parameter y position
    public void Pan(float to)
    {
        down = !down;
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
