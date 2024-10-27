using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SceneManagement;

public class ColliderRespawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision){

        if(collision.CompareTag("Player")){Debug.Log("Health:-10");}
        if(collision.CompareTag("Enemy")){Destroy(collision.gameObject,0.5f);}

    }
}
