using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    private AudioSource musicSource;

    void Awake()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void StopMenuMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
            musicSource.Stop();
    }
}
