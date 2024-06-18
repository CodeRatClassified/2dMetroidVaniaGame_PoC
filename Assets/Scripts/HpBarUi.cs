using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUi : MonoBehaviour
{
    private Entity entity;
    private CharacterAttributes characterAttributes;
    private RectTransform rectTransform;
    private Slider slider;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        slider = GetComponentInChildren<Slider>();
        characterAttributes = GetComponentInParent<CharacterAttributes>();


        entity.onFlipped += FlippedUI;

        characterAttributes.onHealthChanged += UpdateHealthBarUI;
    }


    private void UpdateHealthBarUI()
    {
        slider.maxValue = characterAttributes.GetMaxHealth();
        slider.value = characterAttributes.currentHealth;
    }
    private void FlippedUI()
    {
        rectTransform.Rotate(0, 180, 0);
    }

    private void OnDisable()
    {
        entity.onFlipped -= FlippedUI;
        characterAttributes.onHealthChanged -= UpdateHealthBarUI;
    }
}
