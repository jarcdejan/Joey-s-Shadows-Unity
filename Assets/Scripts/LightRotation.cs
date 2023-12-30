using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{

    public GameObject cameraObj;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //calculate pitch from camera rotation
        Quaternion camRotation = cameraObj.transform.rotation;
        float pitch = Mathf.Atan2(2*camRotation.x*camRotation.w - 2*camRotation.y*camRotation.z, 1 - 2*camRotation.x*camRotation.x - 2*camRotation.z*camRotation.z) * Mathf.Rad2Deg;

        //yaw is inherited from player rotation
        transform.rotation = player.transform.rotation * Quaternion.Euler(pitch, 0, 0);
    }
}
