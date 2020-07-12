using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    Rigidbody2D rb;
    BossHover bh;
    Animator anim;

    public bool invis;
    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bh = anim.GetBehaviour<BossHover>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Cat")) anim.SetBool("Down", true);
        if (collision.gameObject.CompareTag("LeftBound") && bh.travelVelocity < 0) bh.travelVelocity *= -1;
        if (collision.gameObject.CompareTag("RightBound") && bh.travelVelocity > 0) bh.travelVelocity *= -1;
        if (collision.gameObject.CompareTag("Ground")) grounded = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) grounded = false;
    }

    private void OnBecameInvisible()
    {
        invis = true;
    }

    private void OnBecameVisible()
    {
        invis = false;
    }


}
