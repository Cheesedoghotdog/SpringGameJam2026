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
    [SerializeField] Camera MainCam;
    [SerializeField] Vector2 DirectionToMouse;
    [SerializeField] Vector2 MousePos;
    [SerializeField] Vector2 grappleDistanceVector;
    public WebRope grappleRope;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerrigidbody.velocity += new Vector2(Input.GetAxisRaw("Horizontal") * runAccel * Time.deltaTime, 0.0f);
        if (playerrigidbody.velocity.x >= maxSpeed || playerrigidbody.velocity.x <= -maxSpeed) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x * 0.99f, playerrigidbody.velocity.y);
            //This slows the player down if they're going above the max speed.
        }
        if (Input.GetAxisRaw("Horizontal") == 0.0f) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x * 0.98f, playerrigidbody.velocity.y);
            //This slows the player down even faster if they aren't pressing anything.
        }

        if (((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsOnGround())) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, jumpingPower);
            //This causes the player to jump when space, up arrow, or w are pressed.
        }
        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && playerrigidbody.velocity.y > 0f) {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, playerrigidbody.velocity.y * 0.5f);
            //This extends the player jump if they hold the button down.

        }


        MousePos = MainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            DirectionToMouse = (MousePos - (new Vector2(playerrigidbody.position.x, playerrigidbody.position.y))).normalized;
            Debug.Log("there was a click at " + MousePos);
            Debug.Log(DirectionToMouse);
            RaycastHit2D _hit = Physics2D.Raycast(playerrigidbody.position, DirectionToMouse);
            Debug.Log("cccc");
            if (_hit.transform.gameObject.layer != null) {
                Debug.Log("bbbb");
                if (Vector2.Distance(_hit.point, playerrigidbody.position) >= 0) {
                    grappleDistanceVector = _hit.point - (Vector2)playerrigidbody.position;
                    playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x + grappleDistanceVector.x, playerrigidbody.velocity.y + grappleDistanceVector.y);
                    grappleRope.enabled = true;
                    Debug.Log("aaaaa");
                }
            }

        }

    }

    public bool IsOnGround() {
        bool isTrue = groundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
        return isTrue;
        //This checks if the player is on the ground.
    }
}
