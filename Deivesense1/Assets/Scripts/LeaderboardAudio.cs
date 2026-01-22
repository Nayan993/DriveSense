using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }

    void OnDisable()
    {
        StopAudio();
    }

    void OnDestroy()
    {
        StopAudio();
    }

    void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
}
