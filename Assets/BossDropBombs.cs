using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDropBombs : StateMachineBehaviour
{
    public GameObject bomb;

    public float timeToSpendDroppingBombs;
    public float bombDropRate;
    float timeToDropNextBomb;
    float timeElapsed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0;
        timeToDropNextBomb = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeElapsed <= timeToSpendDroppingBombs)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > timeToDropNextBomb)
            {
                //drop a bomb
                Instantiate(bomb, animator.transform.position, Quaternion.identity);
                //set the next bomb drop at current time elapsed + bombDropRate
                timeToDropNextBomb = timeElapsed + bombDropRate;
            }
            
        }
        else
        {
            animator.SetBool("Dropping Bombs", false);
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
