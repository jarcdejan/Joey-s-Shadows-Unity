using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLogic : MonoBehaviour
{   

    public GameObject player;
    public GameObject lightObj;

    public float maxBattery = 100f;
    public float battery = 100f;
    public float batteryLossRate = 1;

    public bool lightOn = true;

    public float baseFlickerChance = 0.1f;

    public float flickerTime = 0.1f;
    private float remainingFilcker = 0f;

    private bool dropped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //decrease battery
        if(lightOn)
            battery = Mathf.Max(battery - batteryLossRate * Time.deltaTime, 0);

        //set the state of the light
        if(lightOn && battery > 0 && remainingFilcker <= 0)
            lightObj.GetComponent<Light>().enabled = true;
        else
            lightObj.GetComponent<Light>().enabled = false;


        //random flicker based on battery level
        float flickerChance = Mathf.Pow((1 - battery / maxBattery), 4) * baseFlickerChance * Time.deltaTime;
        float randomNum = Random.Range(0f, 1f);

        if(lightOn && battery > 0 && randomNum < flickerChance){
            remainingFilcker = flickerTime;
            GetComponent<LightAudioSources>().lightFlicker.Play(0);
        }

        remainingFilcker -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {   
        if(!dropped){
            GetComponent<LightAudioSources>().lightDrop.Play(0);
            dropped = true;
        }
    }
}
