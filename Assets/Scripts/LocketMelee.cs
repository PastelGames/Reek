using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocketMelee : MonoBehaviour
{
   private float attackDiff;
   public float startTimeBtwAttack;
   public Transform attackPos;
   public float attackRange;
   
   public LayerMask whatIsEnemies;
   public int damage;
   void Update()
   {
       if(attackDiff <= 0){
           if(Input.GetKey(KeyCode.W)){
               Debug.Log("AKJSHNKBNDNJIKKLASHLBKDKLLJIKSIAIIDIkl");
               Collider2D [] enemiesDamaged = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
               for (int i =0; i < enemiesDamaged.Length; i++){
                   enemiesDamaged[i].GetComponent<Enemy>().DamageTaken(damage);
               }
           }
           attackDiff = startTimeBtwAttack;
       }

       else{
           attackDiff -= Time.deltaTime;
       }
   }

   void OnDrawGizmosSelected(){
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(attackPos.position, attackRange);
   }
}