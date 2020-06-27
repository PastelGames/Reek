using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    //private bool dirRight = true;
    //public float speed = 1.1f;

    //void Update()
    //{
    //    if (dirRight)
    //        transform.Translate(Vector2.right * speed * Time.deltaTime);
    //    else

    //        transform.Translate(-Vector2.right * speed * Time.deltaTime);

    //    if (transform.position.x >= 1.0f)
    //    {
    //        dirRight = false;
    //    }

    //    if (transform.position.x <= -1)
    //    {
    //        dirRight = true;
    //    }

    public float speed = 0.29f; //Speed of the NPC's walking
    private Rigidbody2D myRigidBody;

    //public float walkTime;
    //private float walkCounter;
    //public float waitTime;
    //private float waitCounter;

    private bool isLeft; // If NPC is moving left 
    private bool isWalking; //If the NPC is walking 

    public Vector3 theNPCPosition;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

        //waitCounter = waitTime;
        //walkCounter = walkTime;

        isLeft = true;
        isWalking = true;

        //obtain the current location
        theNPCPosition = GameObject.Find("Enemy").transform.position;
        Debug.Log(theNPCPosition);

    }

    void Update()
    {
        if (isWalking) //If walking, continue
        {
            if (isLeft) //if left is true, continue walking 
            {
                myRigidBody.velocity = new Vector2(-1 * speed, myRigidBody.velocity.y);
                if(transform.position.x <= 5.8f)
                {
                    myRigidBody.velocity = Vector2.zero;
                    //isWalking = false;
                    isLeft = false;
                }
            }
            else
            {
                myRigidBody.velocity = new Vector2(1 * speed, myRigidBody.velocity.y);
                if (transform.position.x >= theNPCPosition.x)
                {
                    myRigidBody.velocity = Vector2.zero;
                    isLeft = true;
                }

            }
        }
    
    }



}
