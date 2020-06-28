using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouncingState : StateMachineBehaviour
{

    GameObject cat;
    FollowBezierRoute fbr;
    BoxCollider2D col;

    Vector2[] controlPoints;
    public float pounceLength = 5;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat = animator.gameObject;

        fbr = animator.GetComponent<FollowBezierRoute>();

        col = animator.GetComponent<BoxCollider2D>();

        fbr.tParam = 0; //reset the t to 0

        //get the position of the click in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 currentPosition = cat.transform.position;

        //the destination in which you have finished your pounce
        Vector2 pounceDestination = Vector2.ClampMagnitude((mousePosition - currentPosition), pounceLength) + currentPosition;

        //the array of points along the curve that the cat will follow
        controlPoints = new Vector2[] { cat.transform.position, pounceDestination + Vector2.up * 3, pounceDestination };
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fbr.FollowRoute(controlPoints); //follow the route

        //if the cat is currently on the path do not enable the colliders in order to pass through surfaces
        col.enabled = !fbr.following;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Pouncing", false);
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
