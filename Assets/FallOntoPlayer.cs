using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOntoPlayer : StateMachineBehaviour
{

    Rigidbody2D rb;
    public GameObject indicator;
    Boss boss;


    GameObject player;
    Collider2D playerCollider;
    GameObject currentIndicator;
    Collider2D currentIndicatorCollider;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();

        boss = animator.GetComponent<Boss>();

        animator.ResetTrigger("Ready To Fall");

        player = GameObject.FindGameObjectWithTag("Player");

        playerCollider = player.GetComponent<Collider2D>();

        //teleport above the player
        rb.position = new Vector2(player.transform.position.x, animator.transform.position.y);

        //set the indicator where the boss is going to land
        currentIndicator = Instantiate(indicator, player.transform.position, Quaternion.identity);

        currentIndicatorCollider = currentIndicator.GetComponent<Collider2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //when the boss lands
        if (boss.grounded)
        {
            //did the boss hit the player
            if (currentIndicatorCollider.IsTouching(playerCollider))
            {
                //the boss will go back up
                animator.SetBool("Boss Missed", true);
            }
            //did the boss hit the ground
            else
            {
                //the boss missed and is down
                animator.SetBool("Down", true);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(currentIndicator);
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
