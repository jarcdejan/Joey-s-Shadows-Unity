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

    private Light lighto;

    private float initIntensity;
    public float altIntensity;

    // Start is called before the first frame update
    void Start()
    {   
        lighto = transform.GetChild(0).GetComponent<Light>();
        initIntensity = lighto.intensity;
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        if(player.GetComponent<PlayerLogic>().dead){
            return;
        }

        if(raiseLight) {
            transform.localPosition = altPosition;
            lighto.intensity = altIntensity;
        }
        else {
            transform.localPosition = originalPosition;
            lighto.intensity = initIntensity;

        }
    }
}