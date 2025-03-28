using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource; 

    private static MusicManager instance;

    [SerializeField] Slider volumeSlider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
                Load();
            }

            else 
            {
                Load(); 
            }
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

    private void OnEnable()
    {
        DynamicButt.PassMusicSlider += AddSlider;
    }
    private void OnDisable()
    {
        DynamicButt.PassMusicSlider -= AddSlider;
    }

    private void AddSlider(Slider slider)
    {
        volumeSlider = slider;
        slider.value = PlayerPrefs.GetFloat("musicVolume");
        slider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void ChangeVolume() 
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load() 
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save() 
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
