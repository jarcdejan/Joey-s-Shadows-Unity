using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggervicory : MonoBehaviour
{

    public GameObject playerObj;

    private void OnTriggerEnter(Collider other)
    {   
        if(other.gameObject == playerObj) {
            playerObj.GetComponent<PlayerLogic>().Win();
        }
    }
}
