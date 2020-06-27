using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBezierRoute : MonoBehaviour
{
    float tParam = 0;
    float speed = 1;
    public bool active; //are they currently doing the thing
    BoxCollider2D col;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        col.enabled = !active; //if the object is currently going somewhere disable the colliders.
    }

    public IEnumerator FollowRoute(Vector3[] controlPoints)
    {
        active = true; //the object is currently following the routine

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;

            //quadratic bezier curve formula (first derivative) applied to velocity of object
            rb.velocity = 2 * (1 - tParam) * (controlPoints[1] - controlPoints[0]) + 2 * tParam * (controlPoints[2] - controlPoints[1]);

            yield return new WaitForEndOfFrame();
        }

        //reset the t back to zero after it has reached the destination
        tParam = 0;
        
        active = false; //the object has completed its routine
    }

}
