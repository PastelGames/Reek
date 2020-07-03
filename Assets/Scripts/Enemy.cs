using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
    {
    public int health;
    public float speed;
    public bool vulnerable = true;
    
    void Update()
    {
     if(health == 0){
         Destroy(gameObject);
     }
    }

    public void DamageTaken(int damage){
        if (vulnerable)
        {
            health -= damage;
            Debug.Log("enemy has been hit!");
        }
    }
}
