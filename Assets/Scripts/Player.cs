using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;

    CameraFollow cf;
    GameObject cat;

    private void Start()
    {
        cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cat = GameObject.FindGameObjectWithTag("Cat");
    }

    private void Update()
    {
        //if the mouse wheel is scrolled
        if (Mathf.Abs(Input.mouseScrollDelta.y) > .3f)
        {
            //if they are the player at the time of switching then switch to the cat
            if (cf.target == transform) cf.target = cat.transform;
            //if they are the cat at the time of switching switch to the player
            else cf.target = transform;
        }
    }
}
