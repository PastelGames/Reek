using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouncingState : StateMachineBehaviour
{

    GameObject cat;
    FollowBezierRoute fbr;
    BoxCollider2D col;
    Rigidbody2D rb;

    Vector2[] controlPoints;
    public float pounceLength = 5;
    bool followRouteByPosition; //should the cat follow the route using position or velocity?

    Vector2 mousePosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat = animator.gameObject;
        fbr = animator.GetComponent<FollowBezierRoute>();
        col = animator.GetComponent<BoxCollider2D>();
        rb = animator.GetComponent<Rigidbody2D>();

        fbr.tParam = 0; //reset the t to 0

        followRouteByPosition = false;

        //get the position of the click in world space
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 currentPosition = cat.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        //the destination in which you have finished your pounce
        Vector2 pounceDestination = Vector2.zero;

        //if they click anywhere else, stunt the length of the pounce
        if (hit.collider == null || !hit.collider.CompareTag("Player"))
        {
            pounceDestination = Vector2.ClampMagnitude((mousePosition - currentPosition), pounceLength) + currentPosition;
        }
        //if they click the player, they can return the cat back to them from anywhere
        else
        {
            followRouteByPosition = true;
            pounceDestination = mousePosition;
        }
        
        //the array of points along the curve that the cat will follow
        controlPoints = new Vector2[] { cat.transform.position, pounceDestination + Vector2.up * 3, pounceDestination };

        Debug.DrawLine(cat.transform.position, pounceDestination + Vector2.up * 3, Color.white, 1f);
        Debug.DrawLine(pounceDestination + Vector2.up * 3, pounceDestination, Color.white, 1f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (followRouteByPosition) fbr.FollowRouteUsingPosition(controlPoints); //follow the route by position
        else fbr.FollowRoute(controlPoints); //follow the route by velocity

        //if the cat is currently on the path do not enable the colliders in order to pass through surfaces
        if (fbr.tParam < 0.8f) col.enabled = false;
        else col.enabled = true;

        //if they pounce to the player, no matter where they are, they should repounce midair
        if (Input.GetMouseButtonDown(1))
        {
            //get the position of the click in world space
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                //restart the pounce
                animator.SetTrigger("RestartPounce");
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Pouncing", false);
        animator.ResetTrigger("RestartPounce");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
