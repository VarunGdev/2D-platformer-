using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleAIslowAndagressive : MonoBehaviour
{
    float speed;
    GameObject projectile;
    public float projectileSpeed = 10f;
    public float range;
    public Transform PlayerPos;
   

    void Update()
    {
        float distance = Vector2.Distance( this.transform.position,PlayerPos.position);

        if(distance  <= range)
        {
            //shoot without moving
            
        }else{
            
            //Cooldown
            //move a little left and right search
            //Continue Patroling
        }

    }
}
