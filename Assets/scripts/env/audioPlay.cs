using UnityEngine;

public class audioPlay : MonoBehaviour
{
    public AudioClip soundCamera;
    public AudioSource audioSource;

    private void OnMouseDown()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundCamera);
    }
}
