using System.ComponentModel;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public Slider healthSlider;
    public float timeBetweenHealth = 0;
    public static LifeController Instance;

    void Start()
    {
        health = 3;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
        Instance = this;
    }

    private void OnEnable()
    {
        PlayerHP.DamageToBar += TakeDamage;
    }

    private void OnDisable()
    {
        PlayerHP.DamageToBar -= TakeDamage;
    }

    void Update()
    {
        healthSlider.value = health;
    }

    private void TakeDamage(int damage) 
    {
        if (health - damage >= 0) 
        {
            health -= damage;
        }
    }
}