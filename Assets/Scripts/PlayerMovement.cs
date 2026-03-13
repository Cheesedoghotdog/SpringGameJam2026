using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerrigidbody;
    [SerializeField] float runAccel;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpingPower;
    [SerializeField] BoxCollider2D groundCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerrigidbody.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * runAccel, 0.0f);
        if (playerrigidbody.velocity.x >= maxSpeed || playerrigidbody.velocity.x <= -maxSpeed) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x * 0.99f, playerrigidbody.velocity.y);
        }
        if (Input.GetAxisRaw("Horizontal") == 0.0f) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x * 0.9f, playerrigidbody.velocity.y);
        }

        if (((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsOnGround())) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, jumpingPower);
        }
        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && playerrigidbody.velocity.y > 0f) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, playerrigidbody.velocity.y * 0.5f);

        }
    }

    public bool IsOnGround() {
        bool isTrue = groundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
        return isTrue;
    }
}
