using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    private static ScreenManager instance;
    [SerializeField] private Toggle toggle;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (!PlayerPrefs.HasKey("Fullscreen"))
            {
                PlayerPrefs.SetInt("Fullscreen", 1);
                Load();
            }
            else
            {
                Load();
            }

            if (Screen.fullScreen)
            {
                toggle.isOn = true;
            }
            else  
            {
                toggle.isOn = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
        toggle.isOn = fullScreen;   
        Save();
    }

    private void Load()
    {
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            toggle.isOn = true;
        }
        else 
        {
            toggle.isOn = false;    
        }
    }

    private void Save()
    {
        if (toggle.isOn == true)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else 
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        
    }
}
