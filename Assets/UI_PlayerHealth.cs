using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public HealthUI healthUI;





    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.setMaxBalls(maxHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy)
        {
            TakeDamage(enemy.damage);
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthUI.UpdateBalls(currentHealth);

        if(currentHealth <= 0)
        {
            //player dead
        }

    }
}
