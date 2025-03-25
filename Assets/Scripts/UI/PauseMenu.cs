using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPanel;
    private string titleName = "Title";
    private string endName = "Ending";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(false);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == titleName || scene.name == endName)
        {
            menuPanel?.SetActive(false);
        }
    }

    public void OnActivatePauseMenu()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene != titleName && currentScene != endName && menuPanel != null)
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
