using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{

    public GameObject cameraObj;
    public GameObject player;

    public bool raiseLight;
    private Vector3 originalPosition;
    public Vector3 altPosition;
    public Vector3 altRotation;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!raiseLight) {
            //set position
            transform.localPosition = originalPosition;

            //calculate pitch from camera rotation
            Quaternion camRotation = cameraObj.transform.rotation;
            float pitch = Mathf.Atan2(2*camRotation.x*camRotation.w - 2*camRotation.y*camRotation.z, 1 - 2*camRotation.x*camRotation.x - 2*camRotation.z*camRotation.z) * Mathf.Rad2Deg;

            //yaw is inherited from player rotation
            transform.rotation = player.transform.rotation * Quaternion.Euler(pitch, 0, 0);
        }
        else {
            transform.position = cameraObj.transform.position + (cameraObj.transform.right * altPosition.x) + (cameraObj.transform.up * altPosition.y) + (cameraObj.transform.forward * altPosition.z);

            Quaternion camRotation = cameraObj.transform.rotation;

            Quaternion extraRotation = new Quaternion();
            extraRotation.eulerAngles = altRotation;
            transform.rotation = camRotation * extraRotation;
        }
    }
}
