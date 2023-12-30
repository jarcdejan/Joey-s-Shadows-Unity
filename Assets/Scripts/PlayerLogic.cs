using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public GameObject lightObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        //switch flashlight
        if(Input.GetKeyDown("f")) {
            Light light = lightObj.GetComponent<Light>();
            light.enabled = !light.enabled;

            AudioSource audioSource = lightObj.GetComponent<AudioSource>();
            audioSource.Play(0);
        }
    }
}
