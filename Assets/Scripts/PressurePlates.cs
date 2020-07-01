using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour
{
    public bool doorOpen;

    void OnTriggerEnter2D(Collider2D col){
        if(!doorOpen)
        {
            doorOpen = true;
        }
    }
}
