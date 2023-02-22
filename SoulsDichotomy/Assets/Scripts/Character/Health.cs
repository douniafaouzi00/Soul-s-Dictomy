using System;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
 
[Serializable]
public class Health 
{
    [SerializeField]
    private int maxHealth;
    
    [SerializeField]
    private int currentHealth;
    
    public UnityEvent onHeal;
    public UnityEvent onDamage;
    public UnityEvent onDeath;

    public void SetUpHealth()
    {
        currentHealth = maxHealth;
    }

    public void AddHp(int value)
    {
        if (isFullHp())
            return;
        currentHealth = math.min(maxHealth, currentHealth + value);
        onHeal.Invoke();
    }
    public void SubtractHp(int value)
    {
        
        if (currentHealth > 0)
        {
            currentHealth -= value;
            onDamage.Invoke();
            if (currentHealth <= 0)
            {
                Die();
            }
        }
         
    }

    public void Die()
    {
        onDeath.Invoke();
    }
    
    public bool isFullHp()
    {
        return currentHealth == maxHealth;
    }
    
    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
    }

    public float Percentage()
    {
        return (float)currentHealth / (float)maxHealth;
    }

}
