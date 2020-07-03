using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDown : StateMachineBehaviour
{

    Rigidbody2D rb;
    Enemy nme;

    public float downTime = 4f;
    float timeElapsed;
    float healthOnPreviousUpdate;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();

        nme = animator.GetComponent<Enemy>();

        nme.vulnerable = true;

        timeElapsed = 0;

        healthOnPreviousUpdate = nme.health;

        //ignore the collisions with the player and the cat after it has been hit
        Physics2D.IgnoreCollision(animator.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Cat").GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(animator.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>(), true);
        
        //unfreeze the y position
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //keep the boss down for a certain amount of time
        if (timeElapsed < downTime)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            animator.SetBool("Down", false);
            nme.vulnerable = false;
        }

        //check every frame to see if the boss has lost any health
        //if they lost health then recover
        if (nme.health < healthOnPreviousUpdate)
        {
            animator.SetBool("Down", false);
            nme.vulnerable = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
