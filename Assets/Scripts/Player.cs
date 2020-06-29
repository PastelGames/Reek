using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health; 
    public float interactLength;
    public Transform interactPosit;

    void Update(){
        //Interactable Keybind
        if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Interact button was clicked");
            //All objects in the circle 
            Collider2D [] inRangeObjects = Physics2D.OverlapCircleAll(interactPosit.position, interactLength);
            for( int i =0; i<inRangeObjects.Length; i++)
            {
                if(inRangeObjects[i].gameObject.CompareTag("Interactable")){
                       Debug.Log(inRangeObjects[i].name);
                }
             
            }
            


        }
    }
}
