using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRecoverAfterDown : StateMachineBehaviour
{

    float bossAltitude;
    
    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();

        bossAltitude = GameObject.Find("Boss Altitude").transform.position.y;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 positionToReturnTo = new Vector2(animator.transform.position.x, bossAltitude);

        //move the boss back up to the original hovering position
        if (Vector2.Distance(animator.transform.position, positionToReturnTo) > 0.05)
        {
            Vector2 currentPos = animator.transform.position;
            rb.velocity = (positionToReturnTo - currentPos).normalized * 2f;
        }
        else
        {
            animator.SetTrigger("Recovery Complete");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //freeze the y position again
        rb.constraints = rb.constraints | RigidbodyConstraints2D.FreezePositionY;
        
        //recognize the collisions that have been ignored previously
        Physics2D.IgnoreCollision(animator.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Cat").GetComponent<Collider2D>(), false);
        Physics2D.IgnoreCollision(animator.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), false);

        //make the boss invincible for a short period of time
        animator.SetBool("Invincible", true);

        animator.ResetTrigger("Recovery Complete");
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
