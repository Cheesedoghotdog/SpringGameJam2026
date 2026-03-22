using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    public HealthUI healthUI;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D playerrigidbody;
    [SerializeField] MusicManagement Sounds;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.setMaxBalls(maxHealth);
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        playerrigidbody.velocity = new Vector2(-playerrigidbody.velocity.x, -playerrigidbody.velocity.y);

        if(currentHealth <= 0)
        {
            StartCoroutine(Death());
        } else {
            StartCoroutine(FlashBlack());
        }

    }

    private IEnumerator FlashBlack()
    {
        Sounds.PlaySFX("Hurt");
        spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private IEnumerator Death() {
        Sounds.PlaySFX("Death");
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadSceneAsync(0);
    }
}
