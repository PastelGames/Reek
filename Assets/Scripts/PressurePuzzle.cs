using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePuzzle : MonoBehaviour
{
    public bool doorOpen1;
    public GameObject pressure1;
    public GameObject pressure2;
    public GameObject door;

    // Update is called once per frame
    void Update()
    {
        if(pressure1.GetComponent<PressurePlates>().doorOpen && pressure2.GetComponent<PressurePlates>().doorOpen &&  doorOpen1 == false)
        {
            doorOpen1 = true;
            door.transform.position += new Vector3(0,4,0);
        }
    }
}
