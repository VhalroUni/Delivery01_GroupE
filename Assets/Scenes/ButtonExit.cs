using UnityEngine;
using UnityEngine.UI;

public class ButtonExit : MonoBehaviour
{
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        SceneChanger sc = FindAnyObjectByType<SceneChanger>();

        if (sc == null)
        {
            Debug.LogWarning("GameManager no encontrado, creando uno nuevo.");
        }

        button.onClick.AddListener(() => sc.QuitGame());
    }
}
