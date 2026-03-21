using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int health = 10;

    public int currentHealth {get; private set;}
    public int maxHealth {get; private set;}

    public static Action<int> OnPlayerTakeDamage;

    void Awake()
    {
        currentHealth = health;
        maxHealth = health;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        OnPlayerTakeDamage?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
