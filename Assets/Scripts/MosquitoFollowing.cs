using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosquitoFollowing : MonoBehaviour
{
    [SerializeField] private BoxCollider2D wallCheck;
    [SerializeField] private Transform player; 
    [SerializeField] private LayerMask BumpLayer;
    [SerializeField] private float enemyActivationDistance; 
    [SerializeField] private float enemyAcceleration;
    [SerializeField] private float enemyMaxSpeed;
    [SerializeField] private float wallBounce;
    //If the player gets closer than this, the enemy starts following if the enemy is watching for a player.

    [SerializeField] private int enemyStartingMode;
    //0 is Staying Still and watching for player
    //1 is following the player
    private int enemyMode;
    private Rigidbody2D rb;   
    private CapsuleCollider2D enemyCollider;

    void Start()
    {
        int enemyMode = enemyStartingMode;
        rb = GetComponent<Rigidbody2D>();
        enemyCollider = GetComponent<CapsuleCollider2D>();
    }


    void Update()
    {
        if (enemyMode == 0) {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            if (distanceToPlayer <= enemyActivationDistance) {
                enemyMode = 1;
            }
        }
        if (enemyMode == 1) {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity += new Vector2((direction.x * enemyAcceleration), (direction.y * enemyAcceleration));
            if (wallCheck.IsTouchingLayers(BumpLayer)) {
                wallCheck.enabled = false;
                enemyCollider.enabled = false;
                if (Physics2D.Raycast(transform.position, new Vector2(1.0f, 0.0f), 1.0f, BumpLayer)) {
                    rb.velocity = new Vector2(rb.velocity.x - wallBounce, rb.velocity.y);
                }
                if (Physics2D.Raycast(transform.position, new Vector2(-1.0f, 0.0f), 1.0f, BumpLayer)) {
                    rb.velocity = new Vector2(rb.velocity.x + wallBounce, rb.velocity.y);
                }
                if (Physics2D.Raycast(transform.position, new Vector2(0.0f, 1.0f), 1.0f, BumpLayer)) {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - wallBounce);
                }
                if (Physics2D.Raycast(transform.position, new Vector2(0.0f, -1.0f), 1.0f, BumpLayer)) {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + wallBounce);
                }
                wallCheck.enabled = true;
                enemyCollider.enabled = true;
            }
            if (Mathf.Abs(rb.velocity.x) >= enemyMaxSpeed) {
            rb.velocity = new Vector2(rb.velocity.x * 0.999f, rb.velocity.y);
            }
            if (Mathf.Abs(rb.velocity.y) >= enemyMaxSpeed) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.999f);
            }
            if (rb.velocity.x < 0) {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            } else {
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
}
