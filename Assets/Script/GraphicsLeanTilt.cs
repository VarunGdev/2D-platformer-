using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsLeanTilt : MonoBehaviour
{
    
    void Update()
    {
        float Horizontal =Input.GetAxisRaw("Horizontal");

        if (Horizontal >0.1)
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,transform.rotation.y,-7f));

        }else if (Horizontal < -0.1)
        {

            this.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,transform.rotation.y,7f));

        }else
        {
            this.transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x,transform.rotation.y,0f));
        }
    }
}
