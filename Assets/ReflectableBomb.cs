using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectableBomb : MonoBehaviour
{

    FollowBezierRoute fbr;
    Bomb bomb;
    GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GameObject.Find("Boss").GetComponent<Collider2D>());

        fbr = GetComponent<FollowBezierRoute>();
        bomb = GetComponent<Bomb>();
        boss = GameObject.Find("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if it collides with the cat and the cat is currently pouncing
        if (collision.gameObject.CompareTag("Cat") && GameObject.FindGameObjectWithTag("Cat").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Pouncing"))
        {
            //set the control points
            Vector2[] cps = { transform.position, //starting position
            ((boss.transform.position - transform.position) / 2) + transform.position + Vector3.up * (boss.GetComponent<Animator>().GetBehaviour<TossingBombs>().bombTossHeight / 2), // halfway to the boss + up + position
            boss.transform.position}; //players position

            //stop the coroutine that is currently running
            fbr.StopAllCoroutines();

            //move toward the boss
            fbr.tParam = 0;
            fbr.StartFollowRouteRoutine(cps);
            fbr.speed = boss.GetComponent<Animator>().GetBehaviour<TossingBombs>().bombSpeed;

            //allow collisions with the boss again
            Physics2D.IgnoreCollision(boss.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }

        //if the boss is currently tossing bombs and the reflectable bomb hits the boss then make the boss take damage
        else if (collision.gameObject.CompareTag("Boss") && boss.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Tossing Bomb"))
        {
            //boss takes damage if hit by reflected bomb
            boss.GetComponent<Enemy>().DamageTaken(1);
            boss.GetComponent<Animator>().SetInteger("Health", boss.GetComponent<Enemy>().health);
            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Bomb"))
        {
            //ignore the collision
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
