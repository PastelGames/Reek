using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : StateMachineBehaviour
{
    float invincibilityTime = 4f;
    float timeElapsed;

    Collider2D col;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0; //reset time elapsed;
        col = animator.GetComponent<Collider2D>();

        //ignore collisions with the cat while invincible
        Physics2D.IgnoreCollision(col, GameObject.FindGameObjectWithTag("Cat").GetComponent<Collider2D>(), true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeElapsed < invincibilityTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            animator.SetBool("Invincible", false); //the boss will not be invincible after the time is up
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //the boss can now collide with the cat once again
        Physics2D.IgnoreCollision(col, GameObject.FindGameObjectWithTag("Cat").GetComponent<Collider2D>(), false);
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
