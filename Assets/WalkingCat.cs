using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCat : StateMachineBehaviour
{

    Rigidbody2D rb;
    Vector2 targetLocation;
    GameObject cat;

    public float catWalkSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        cat = animator.gameObject;

        //the target location is where the player clicked
        targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //allow the player to pounce by pressing the right click
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("Pouncing", true);
        }

        //if the mouse is clicked in the middle of walking to the destination, set a new target location
        if (Input.GetMouseButtonDown(0))
        {
            targetLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        //if the cat is too far away, move towards the target location
        if(Mathf.Abs(cat.transform.position.x - targetLocation.x) > 0.1)
        {
            //make the object move towards the target location on the x axis
            rb.velocity = new Vector2((targetLocation - new Vector2(cat.transform.position.x, cat.transform.position.y)).normalized.x * catWalkSpeed, rb.velocity.y);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Walking", false);
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
