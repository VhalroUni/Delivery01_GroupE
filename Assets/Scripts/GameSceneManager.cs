using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance { get; private set; }
    private string nextSceneName;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(bool loadCurrentScene)
    {
        if (loadCurrentScene)
        {
            nextSceneName = SceneManager.GetActiveScene().name;
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
