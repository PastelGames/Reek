using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
    {
    public int health;
    public float speed; 
    
    void Update()
    {
     if(health == 0){
         Destroy(gameObject);
     }
    }
    public void DamageTaken(int damage){
        health -= damage;
        Debug.Log("enemy has been hit!");
    }
}
