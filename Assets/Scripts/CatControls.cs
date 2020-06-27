using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControls : MonoBehaviour
{

    FollowBezierRoute fbr;
    bool grounded; //variable that stores whether the cat is touching the ground or not

    // Start is called before the first frame update
    void Start()
    {
        fbr = GetComponent<FollowBezierRoute>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!fbr.active && grounded) //the player can jump if they are not currently jumping
            {
                //get the position of the click in world space
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                //the array of points along the curve that the cat will follow
                Vector3[] controlPoints = { transform.position, mousePosition + Vector2.up * 3, mousePosition };

                //start tracing the path
                StartCoroutine(fbr.FollowRoute(controlPoints));
            }
        }   
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if the player is grouned
        if(collision.gameObject.tag.Equals("Cat Ground") || collision.gameObject.tag.Equals("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if the player leaves the ground
        if (collision.gameObject.tag.Equals("Cat Ground") || collision.gameObject.tag.Equals("Ground"))
        {
            grounded = false;
        }
    }
}
