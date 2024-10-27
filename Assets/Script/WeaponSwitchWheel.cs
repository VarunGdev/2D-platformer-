using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchWheel : MonoBehaviour
{
    private string[] Weapons = new string[]{"A","B","C"};
    private float scrollInput;
    

    private void Update()
    {  
        scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scrollInput) > Mathf.Epsilon)
        {

            scrollInput = 0f;
        }
    }

}
