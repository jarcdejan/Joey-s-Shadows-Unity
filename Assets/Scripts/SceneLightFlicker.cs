using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLightFlicker : MonoBehaviour
{

    public float flickerChance = 0.1f;

    public float flickerTime = 0.1f;
    private float remainingFilcker = 0f;

    public bool lightOn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void turnOn(bool on) {
        foreach (Transform child in transform) {
            Light lightSource = child.GetComponent<Light>();
            if(lightSource != null){
                lightSource.enabled = on;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //set the state of the light
        if(lightOn && remainingFilcker <= 0)
            turnOn(true);
        else
            turnOn(false);


        //random flicker based on battery level
        float randomNum = Random.Range(0f, 1f);

        if(lightOn && randomNum < flickerChance * Time.deltaTime){
            remainingFilcker = flickerTime;
            GetComponent<AudioSource>().Play(0);
        }

        remainingFilcker -= Time.deltaTime;
    }
}
