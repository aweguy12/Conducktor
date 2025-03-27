using UnityEngine;
using UnityEngine.UI; 

public class CameraSlideDown : MonoBehaviour
{
    public Camera mainCamera; 
    public Button slideButton; 
    public Vector3 targetPosition; 
    public float slideDuration = 2f; 

    private Vector3 startPosition;
    private float elapsedTime = 0f;
    private bool isSliding = false;

    void Start()
    {
        startPosition = mainCamera.transform.position;
        slideButton.onClick.AddListener(StartSlide);
    }

    void StartSlide()
    {
        isSliding = true;
        elapsedTime = 0f;
    }

    void Update()
    {
        if (isSliding)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / slideDuration); // Normalized time (0 to 1)

            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            if (t >= 1f)
            {
                isSliding = false;
            }
        }
    }
}
