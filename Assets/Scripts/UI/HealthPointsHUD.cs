using UnityEngine;
using UnityEngine.UI;

public class HealthPointsHUD : MonoBehaviour
{
    private Text hpText;
    private PlayerHP playerHP;
    private void Awake()
    {
        hpText = GetComponent<Text>();
        playerHP = GameObject.FindWithTag("Player").GetComponent<PlayerHP>();
    }
  
    private void Update()
    {
        UpdateHPText();
    }
    
      private void UpdateHPText()
    {
        hpText.text = "HP: " + playerHP.playerCurrentHP.ToString("D2");
    }
}