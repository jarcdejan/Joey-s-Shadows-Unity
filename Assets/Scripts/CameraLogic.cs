using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{

    public GameObject player;
    
    public float maxFOV = 55f;
    public float minFOV = 50f;

    public float maxChangeRateFOV = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if(player.GetComponent<PlayerLogic>().dead){
            return;
        }

        //change FOV based on sanity
        float sanityMax = player.GetComponent<PlayerLogic>().maxSanity;
        float sanity = player.GetComponent<PlayerLogic>().sanity;

        float targetFOV = (sanity / sanityMax) * (maxFOV - minFOV) + minFOV;

        float currFOV = GetComponent<Camera>().fieldOfView;


        GetComponent<Camera>().fieldOfView = currFOV + (targetFOV - currFOV) * Time.deltaTime;
    }
}
