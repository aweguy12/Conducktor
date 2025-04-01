/*
 * Name: Jack Gu
 * Date: 4/1/25
 * Desc: Pans the camera with speed up and slow down
 */

using UnityEngine;

public class PanCamera : MonoBehaviour
{
    [Tooltip("The max value for lerp reached at the center of the pan.")]
    [Range(0f, 1f)]
    public float maxLerp;

    public bool panStart = false;
    private bool panEnd = false;
    private float from;
    public float to;
    private float t = 0.01f;

    private void Start()
    {
        from = transform.position.y;
    }

    private void FixedUpdate()
    {
        if (panStart)
        {
            t = Mathf.Lerp(t, maxLerp, 1 - (transform.position.y - (from + to)) / 2 / ((from - to) / 2));
            transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, to, t), transform.position.z);

            if (Mathf.Abs(transform.position.y - (from + to) / 2) < 0.01)
            {
                panStart = false;
                panEnd = true;
                transform.position = new Vector3(transform.position.x, (from + to) / 2, transform.position.z);
                t = maxLerp;
            }
        }
        else if (panEnd)
        {
            t = Mathf.Lerp(maxLerp, 0, Mathf.Abs(transform.position.y - (from + to) / 2) / Mathf.Abs((from - to) / 2));
            transform.position = new Vector3(0, Mathf.Lerp(transform.position.y, to, t), transform.position.z);

            if (Mathf.Abs(transform.position.y - to) < 0.01)
            {
                panEnd = false;
                transform.position = new Vector3(transform.position.x, to, transform.position.z);
                t = 0.01f;
            }
        }
    }

    // Pans camera to given parameter y position
    public void Pan(float to)
    {
        panStart = true;
        this.to = to;
    }
}
