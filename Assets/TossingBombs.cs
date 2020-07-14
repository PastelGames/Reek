using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossingBombs : StateMachineBehaviour
{

    public float timeBetweenTossing;
    float downTime;
    public float bombTossHeight = 5; //the height of the second control point in the toss
    public float bombSpeed;

    public GameObject bomb;
    GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        downTime = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        animator.ResetTrigger("Boss Fight Sequence 2");

        animator.GetComponent<Enemy>().vulnerable = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //toss a bomb every time timeBetweenTossing has passed
        if (downTime > timeBetweenTossing)
        {
            TossBomb(animator);
            Debug.Log("Bomb Tossed");
            downTime = 0;
        }

        downTime += Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //reset all triggers
        animator.ResetTrigger("Boss Fight Sequence 2");
        animator.ResetTrigger("Recovery Complete");
        animator.ResetTrigger("Boss Fight Started");
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

    void TossBomb(Animator animator)
    { 
        //instantiate the bomb at the boss
        GameObject newBomb = Instantiate(bomb, animator.transform.position, Quaternion.identity);

        //set the control points
        Vector2[] cps = { newBomb.transform.position, //starting position
            ((player.transform.position - newBomb.transform.position) / 2) + newBomb.transform.position + Vector3.up * bombTossHeight, //halfway to the player + up + starting position
            player.transform.position}; //players position

        //follow the route given by the control points
        newBomb.GetComponent<FollowBezierRoute>().StartFollowRouteRoutine(cps);
        newBomb.GetComponent<FollowBezierRoute>().speed = bombSpeed;
    }
}
