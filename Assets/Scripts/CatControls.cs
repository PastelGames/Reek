using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControls : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if the player is grouned
        if(collision.gameObject.tag.Equals("Cat Ground") || collision.gameObject.tag.Equals("Ground"))
        {
            anim.SetBool("Grounded", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if the player leaves the ground
        if (collision.gameObject.tag.Equals("Cat Ground") || collision.gameObject.tag.Equals("Ground"))
        {
            anim.SetBool("Grounded", false);
        }
    }
}
