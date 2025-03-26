using UnityEngine;
using UnityEngine.UI;

public class ControlSound : MonoBehaviour
{
    public AudioSource soundSource;

    public static ControlSound instance;

    [SerializeField] Slider volumeSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("soundsVolume"))
            {
                PlayerPrefs.SetFloat("soundsVolume", 1);
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
        soundSource = GetComponent<AudioSource>();
    }

     private void OnEnable()
     {
        DynamicButt.PassAudioSlider += AddSlider;
     }

    private void OnDisable()
    {
        DynamicButt.PassAudioSlider -= AddSlider;
    }

    private void AddSlider(Slider slider) 
    {
        volumeSlider = slider;
        slider.value = PlayerPrefs.GetFloat("soundsVolume");
        slider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void RunSound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }

    public void ChangeVolume()
    {
        soundSource.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("soundsVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("soundsVolume", volumeSlider.value);
    }
}