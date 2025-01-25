using UnityEngine;
using UnityEngine.UI;

public class HealthPointsHUD : MonoBehaviour
{
    private Text HPText;
    private PlayerHP playerHP;
    private void Awake()
    {
        HPText = GetComponent<Text>();
        playerHP = GameObject.FindWithTag("Player").GetComponent<PlayerHP>();
    }
    private void Start()
    {
        //UpdateHPText();
    }

    private void UpdateHPText()
    {
        HPText.text = "HP: " + playerHP.playerCurrentHP.ToString("D2");
    }
    private void Update()
    {
        UpdateHPText();
    }
}