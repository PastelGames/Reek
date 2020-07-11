using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBezierRoute : MonoBehaviour
{
    public float tParam = 0;
    public float speed = 1;
    public bool following; //is the path being followed currently

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(rb.velocity);
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

        //Debug.Log(controlPoints[0].ToString() + controlPoints[1].ToString() + controlPoints[2].ToString());

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

    public IEnumerator FollowRouteRoutine(Vector2[] controlPoints)
    {
        // Debug.Log(controlPoints[0].ToString() + controlPoints[1].ToString() + controlPoints[2].ToString());
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;

            //quadratic bezier curve formula (first derivative) applied to velocity of object
            rb.position = (1 - tParam) * ((1 - tParam) * controlPoints[0] + tParam * controlPoints[1])
                + tParam * ((1 - tParam) * controlPoints[1] + tParam * controlPoints[2]);

            following = true;

            yield return new WaitForEndOfFrame();
        }
        following = false;
    }

    public void StartFollowRouteRoutine(Vector2[] controlPoints)
    {
        StartCoroutine(FollowRouteRoutine(controlPoints));
    }

}
