/*
 * Name: Jack Gu
 * Date: 4/3/25
 * Desc: Fades in and out background music based on position
 */

using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = transform.position.y / 27;
    }
}
