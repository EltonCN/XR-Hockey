using UnityEngine;
using System.Linq;

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
