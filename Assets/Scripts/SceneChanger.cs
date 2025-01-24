using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            QuitGame();
        }
        else if (Input.GetKey("return"))
        {
            LoadGame();
        }

        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void LoadEnd()
    {
        SceneManager.LoadScene("Ending");
    }
}
