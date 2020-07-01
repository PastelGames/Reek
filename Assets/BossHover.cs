using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHover : StateMachineBehaviour
{

    public float travelVelocity;
    Vector2 lastPos;
    float threshold = .001f;

    Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();

        lastPos = animator.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //make the object coast to the right
        rb.velocity = Vector2.right * travelVelocity;

        //enforces movement of the object no more getting stuck yay!!!
        if (!IsMoving(animator))
        {
            travelVelocity *= -1;
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

    //returns true if the item has moved in the last frame
    bool IsMoving(Animator animator)
    {
        Vector2 currentPos = animator.transform.position;
        float offset = Vector2.Distance(currentPos, lastPos);
 
        //has the object not moved?
        if (Mathf.Abs(offset) < threshold)
        {
            return false;
        }
        else
        {
            lastPos = animator.transform.position;
            return true;
        }
    }
}
