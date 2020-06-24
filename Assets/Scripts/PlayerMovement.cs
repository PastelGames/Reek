using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this controls the players movement
public class PlayerMovement : MonoBehaviour
{
    public float jumpForce = 1000;
    public float xVelocity;
    public float moveSpeed;
    float movementValue;
    bool grounded;
    Rigidbody2D rb;
    BoxCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        //player jumping
        //only allow jumping if grounded
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //set the value for the velocity of the x
        xVelocity = Mathf.Clamp(GetLeftRightMovement() * moveSpeed, moveSpeed * -1, moveSpeed);
        //update the horizontal movement of the player.
        rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }

    //returns a float with the direction in which the player should be moving
    //A is left, D is right
    float GetLeftRightMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return -1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return 1f;
        }
        else
        {
            return 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //when the player is on the ground the grounded boolean should be true
        if (collision.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //when the player leaves the ground they are no longer grounded
        if (collision.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
        }
    }
}
