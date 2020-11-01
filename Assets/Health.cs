using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public float maxHealth = 1000;
    public float currentHealth = 1000;

    [Server]
    public void Damage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    [Server]
    public void Cure(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    } 

    [Server]
    void Die()
    {
        NetworkServer.Destroy(gameObject);
    }

}
