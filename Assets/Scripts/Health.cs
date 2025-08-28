using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    float currentHealth, minHealth = 0, maxHealth = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxHealth()
    { 
        return maxHealth; 
    }

    public void ApplyDamage(float damage, int ID)
    {
        currentHealth -= damage;

        if (currentHealth < minHealth)
        {
            currentHealth = minHealth;
        }

        if (currentHealth == minHealth)
        {
            MyEvents.AddScore.Invoke(ID);
        }
    }

    public void HealDamage(float damage)
    {
        currentHealth += damage;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
