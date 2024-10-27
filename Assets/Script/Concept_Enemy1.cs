using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Concept_Enemy1 : MonoBehaviour
{
    private bool isActive = false;
    public Transform body;
    public Transform rope;
    public float Time;

    void Start()
    {   

        StartCoroutine(WaitAndPrint(Time));

    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
       while(true)
       {
         if (isActive)
         {
            rope.localScale = new UnityEngine.Vector2(rope.localScale.x,25f);
            body.localScale = new UnityEngine.Vector2(1.5f,body.localScale.y);
            isActive = false;

         }else
         {
            rope.localScale = new UnityEngine.Vector2(rope.localScale.x,0f);
            body.localScale = new UnityEngine.Vector2(2f,body.localScale.y);
            isActive = true;
         }

         yield return new WaitForSeconds(waitTime);
     
        }

    }
}
