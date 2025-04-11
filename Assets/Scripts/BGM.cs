/*
 * Name: Jack Gu
 * Date: 4/10/25
 * Desc: Receives 
 */

using UnityEngine;

public class BGM : MonoBehaviour
{
    [Tooltip("BGM with fade-out at end, should be ~1:15 long.")]
    public AudioClip bgm;

    [Tooltip("BGM that loops.")]
    public AudioClip bgmLoop;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOnce()
    {
        audioSource.clip = bgm;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayLoop()
    {
        audioSource.clip = bgmLoop;
        audioSource.loop = true;
        audioSource.Play();
    }
}
