using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource; 

    private static MusicManager instance; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true; 
            musicSource.Play();
        }
    }
}
