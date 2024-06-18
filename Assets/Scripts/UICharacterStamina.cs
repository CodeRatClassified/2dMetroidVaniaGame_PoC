using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterStamina : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (playerStats != null)
            playerStats.onStaminaChanged += UpdateStaminaBarUi;

    }

    private void Update()
    {
       
    }

    private void UpdateStaminaBarUi()
    {
        slider.maxValue = playerStats.GetMaxStamina();
        slider.value = playerStats.currentStamina;
    }
}
