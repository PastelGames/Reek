using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this controls the players movement
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    float movementValue;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //update the horizontal movement of the player.
        rb.velocity = Vector3.ClampMagnitude( new Vector2((GetLeftRightMovement() * moveSpeed) + rb.velocity.x, rb.velocity.y), moveSpeed);
    }

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
}
