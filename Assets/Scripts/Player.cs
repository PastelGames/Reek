using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public int health;
    public float interactLength;
    public Transform interactPosit;
    Dialogue dialogue;

    CameraFollow cf;
    GameObject cat;

    private void Start()
    {
        cf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        cat = GameObject.FindGameObjectWithTag("Cat");
        dialogue = GameObject.Find("Dialogue Manager").GetComponent<Dialogue>();
    }

    void Update()
    {
        //Interactable Keybind
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact button was clicked");
            //All objects in the circle 
            Collider2D[] inRangeObjects = Physics2D.OverlapCircleAll(interactPosit.position, interactLength);
            for (int i = 0; i < inRangeObjects.Length; i++)
            {
                if (inRangeObjects[i].gameObject.CompareTag("Interactable"))
                {
                    Debug.Log(inRangeObjects[i].name);
                }
                if (inRangeObjects[i].gameObject.CompareTag("NPCDialogue"))
                {
                    StartCoroutine(dialogue.Type());
                }
            }

        }
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
