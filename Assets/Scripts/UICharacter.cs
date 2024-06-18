using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacter : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (playerStats != null)
            playerStats.onHealthChanged += UpdateHealthBarUI;
    }

    private void Update()
    {
        
    }

    private void UpdateHealthBarUI()
    {
        slider.maxValue = playerStats.GetMaxHealth();
        slider.value = playerStats.currentHealth;
    }

}
