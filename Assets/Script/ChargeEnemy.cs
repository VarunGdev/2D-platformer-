using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class ChargeEnemy : MonoBehaviour
{ 
   public Transform target;
   public Transform body;       
   public float detectionRange = 10f;
   public float chargeSpeed = 15f;    
   public float chargeDuration = 1f;  
   public float cooldownTime = 1f;    
   public float damage = 50f;      
   public float HitForce =200f;   
   private bool isCharging = false;
   private float chargeTime = 0f;
   private float cooldownTimer = 0f;
   private Vector3 chargeDirection;

   void Update()
   {   
      FaceTowardsPlayer();

      if (!isCharging)
      {
         
         if (Vector3.Distance(transform.position, target.position) <= detectionRange)
         {
            StartCharge();
         } 
      }
      else
      { 
       Charge();
      }

      if (cooldownTimer > 0f)
      { 
         cooldownTimer -= Time.deltaTime;
      }
   }

   void FaceTowardsPlayer()
   {
      Vector2 dis = target.position-transform.position;
      dis.Normalize();
       
      if (dis.x > 0.01f && !isCharging)
      {
         this.GetComponent<SpriteRenderer>().flipY =true;
         body.localScale = new Vector2(-0.5f,body.localScale.y);
         
      }else if (dis.x < 0.01f && !isCharging)
      {
         this.GetComponent<SpriteRenderer>().flipY =false;
         body.localScale = new Vector2(0.5f,body.localScale.y);
      }
   }

   void StartCharge()
   {
      if (cooldownTimer <= 0f)
      {
         isCharging = true;
         chargeTime = 0f;
         chargeDirection = (new Vector2(target.position.x,0f) - new Vector2(transform.position.x,0f)).normalized;
         Debug.Log("Enemy starts charging!");
      }
   }

   void Charge()
   {
      if (chargeTime < chargeDuration)
      {
         transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
         chargeTime += Time.deltaTime;
         if (Vector3.Distance(transform.position, target.position) <= 1f)
         {
            Debug.Log("Enemy hits the target for " + damage + " damage!");
            isCharging = true;
            cooldownTimer = cooldownTime; 
         }
      }
      else
      {
         isCharging = false;
         cooldownTimer = cooldownTime;
         Debug.Log("Enemy finished charging.");
      }
    }

   void OnTriggerEnter2D(Collider2D collision)
   {
      if(collision.transform.CompareTag("Player"))
      {
        target.position = new Vector2(target.position.x,target.position.y+HitForce);
      }

   }

   void OnDrawGizmosSelected()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, detectionRange);
   }

}

   

