using UnityEngine;
using UnityEngine.UI;

public class DynamicButt : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick() 
    {
        SceneChanger sc = FindAnyObjectByType<SceneChanger>();

        switch (gameObject.name) 
        {
            case "StartButt": 
            sc.LoadGame();
            break;

            case "ExitButt":
            sc.QuitGame();
            break;

            case "MenuButt":
            sc.LoadMenu();
            break;

            default:
            Debug.Log("This Button does nothing");
            break;
        }
    }
}
