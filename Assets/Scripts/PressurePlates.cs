using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlates : MonoBehaviour
{
    public bool doorOpen;

    void OnTriggerStay2D(Collider2D col)
    {
        doorOpen = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        doorOpen = false;
    }
}
