using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

public class Slash_Attack : MonoBehaviour
{   
    public GameObject Pointer;
    private Vector3 dragStartPos,dragEndPos;
    private bool isDragging = false;
    public Transform Pivot;
    private GameObject mouse;

    void Update()
    {  
        Slash(); 
    }
    
    void Slash()
    {   
        Vector3 WorldPosition = GetMouseWorldPosition();
        if(mouse != null){mouse.transform.position = WorldPosition;}

        if (Input.GetMouseButtonDown(0)) 
        {   
            mouse = Instantiate(Pointer,WorldPosition, Quaternion.identity);
            dragStartPos = WorldPosition;
            isDragging = true;    
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {   
            dragEndPos = WorldPosition;
            PerformAttack(dragStartPos, dragEndPos);
            isDragging = false; 
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if(mouse != null) Destroy(mouse);
        }
    }

    void PerformAttack(Vector3 dragStartPos,Vector3 dragEndPos)
    {   
       
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider!=null)
        {

            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Hit 2D object: " + hitObject.name);
                transform.DOMove(hitObject.transform.position + transform.forward,0.5f);
                Destroy(hit.collider.gameObject,0.4f);
            }else
            {

                Debug.Log("Hit object is NOT on the target layer: " + hit.collider.gameObject.name);
            } 
        }
    }


    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; 
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    

}
