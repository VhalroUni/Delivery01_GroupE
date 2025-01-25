using UnityEngine;

public class ControlSound : MonoBehaviour
{
    public static ControlSound instance;

    private AudioSource AudioSource;


    private void Awake()
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
        AudioSource = GetComponent<AudioSource>();
    }

    public void RunSound(AudioClip sound)
    {
        AudioSource.PlayOneShot(sound);
    }
}
