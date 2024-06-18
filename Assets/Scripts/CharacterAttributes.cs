using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttributes : MonoBehaviour
{
    public Stat maxHealth;
    public Stat maxStamina;

    public Stat damage;
    public Stat critChance;
    public Stat critDamageX;

    public Stat strength;
    public Stat vitality;
    public Stat endurance;


    [SerializeField] public int currentHealth;
    [SerializeField] public int currentStamina;

    public System.Action onHealthChanged;
    public System.Action onStaminaChanged;

    [SerializeField] private float staminaRegenRate = 5f;
    private float staminaRegenTimer = 0f;
    private bool isRegeneratingStamina = true;

    public int attackStaminaCost = 10;
    public int dashStaminaCost = 15;

    protected virtual void Start()
    {
        currentHealth = GetMaxHealth();
        currentStamina = GetMaxStamina();
        
    }
    protected virtual void Update()
    {
        if (isRegeneratingStamina)
            RegenerateStamina();
    } 

    public virtual void DoDamage(CharacterAttributes _targetAttributes)
    {


        int totalDamage = damage.GetValue() + strength.GetValue();
        _targetAttributes.TakeDamage(totalDamage);
    }

    public virtual void TakeDamage(int _damage)
    {
        DecreaseHealthBy(_damage);

        if (currentHealth <= 0)
            Die();

        
    }

    protected virtual void DecreaseHealthBy(int _damage)
    {
        currentHealth -= _damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {
       
    }

    private void RegenerateStamina()
    {
        if (currentStamina < GetMaxStamina())
        {
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= 1f)
            {
                currentStamina += Mathf.FloorToInt(staminaRegenRate * staminaRegenTimer);
                staminaRegenTimer = 0f;
                currentStamina = Mathf.Min(currentStamina, GetMaxStamina());
                if (onStaminaChanged != null)
                    onStaminaChanged();
            }
        }
    }

    public void UseStamina(int amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            if (onStaminaChanged != null)
                onStaminaChanged();
        }
    }

    public void StartStaminaRegeneration()
    {
        isRegeneratingStamina = true;
    }

    public void StopStaminaRegeneration()
    {
        isRegeneratingStamina = false;
        staminaRegenTimer = 0;
    }

    public int GetMaxHealth()
    {
        return maxHealth.GetValue();
    }

    public int GetMaxStamina()
    {
        return maxStamina.GetValue();
    }

}
