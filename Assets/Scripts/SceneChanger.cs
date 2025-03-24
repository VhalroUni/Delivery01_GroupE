using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    private const string titleSceneName = "Title", gameplaySceneName = "Gameplay", endingSceneName = "Ending";
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
    }

    void OnStart() 
    {
        if (ScoreSystem.instance != null)
        {
            ScoreSystem.instance.ResetScore();
        }
        LoadGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
    public void LoadEnd()
    {
        SceneManager.LoadScene(endingSceneName);
    }
}
