using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBezierRoute : MonoBehaviour
{
    public float tParam = 0;
    float speed = 1;
    public bool following; //is the path being followed currently

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FollowRouteUsingPosition(Vector2[] controlPoints)
    {
        if (tParam < 1)
        {
            tParam += Time.deltaTime * speed;

            //quadratic bezier curve formula applied to position of object
            transform.position = (1 - tParam) * ((1 - tParam) * controlPoints[0] + tParam * controlPoints[1])
                + tParam * ((1 - tParam) * controlPoints[1] + tParam * controlPoints[2]);

            following = true;
        }
        else
        {
            following = false;
        }
    }

    public void FollowRoute(Vector2[] controlPoints)
    {
        if (tParam < 1)
        {
            tParam += Time.deltaTime * speed;

            //quadratic bezier curve formula (first derivative) applied to velocity of object
            rb.velocity = 2 * (1 - tParam) * (controlPoints[1] - controlPoints[0]) + 2 * tParam * (controlPoints[2] - controlPoints[1]);

            following = true;
        }
        else
        {
            following = false;
        }
    }

}
