using UnityEngine;
using System.Linq;

/// <summary>
/// This class is responsible for playing an audio whenever there is a collision.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class PlayAudioOnHit : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] string[] ignoreTags;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (! ignoreTags.Contains(collision.gameObject.tag))
            audioSource.Play();
    }
}
