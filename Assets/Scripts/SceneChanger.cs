using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /*//!public delegate void SceneLoadedDelegate(string sceneName);
    public static event SceneLoadedDelegate OnSceneLoaded;*/

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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();
        }
        else if (Input.GetKey(KeyCode.Return))
        {
            if (SceneManager.GetActiveScene().name != gameplaySceneName)
            {
                LoadGame();
            }
        }



    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadGame()
    {
        ScoreSystem.instance.ResetScore();
        SceneManager.LoadScene(gameplaySceneName);
        //!OnSceneLoaded?.Invoke(gameplaySceneName);
    }
    public void LoadEnd()
    {
        SceneManager.LoadScene(endingSceneName);
        //!OnSceneLoaded?.Invoke(endingSceneName);
    }

    /*//!public string GetTitleName(int sceneNumber)
    {
        switch(sceneNumber)
        {
            case 1: return titleSceneName;
            case 2: return gameplaySceneName;
            case 3: return endingSceneName;
            default: return titleSceneName;
        }
    }*/
}
