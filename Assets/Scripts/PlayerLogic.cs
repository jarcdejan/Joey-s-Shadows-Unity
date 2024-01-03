using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{

    public GameObject lightObj;
    public GameObject cameraObj;

    public float rayCastDistance = 10f;
    public LayerMask raycastMask;
    public GameObject crosshair;
    public GameObject crosshairAlt;

    public int batteryCount = 0;
    public int pillCount = 0;

    public float maxSanity = 100f;
    public float sanity = 100f;
    public float sanityLossRate = 1;
    public float maxBattery = 100f;
    public float battery = 100f;
    public float batteryLossRate = 1;

    public GameObject itemAudioNode;
    public GameObject whispersAudioNode;

    private GameObject holdingObj;
    private List<GameObject> inventory = new List<GameObject>();

    private bool lightOn = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void setCrosshairAlt(bool isAlt)
    {
        crosshair.SetActive(!isAlt);
        crosshairAlt.SetActive(isAlt);
    }

    void handleObjectPickup(GameObject obj, ObjData objData)
    {
        Debug.Log(objData.objType);

        if(objData.objType is ObjTypes.battery){
            batteryCount += 1;
            obj.SetActive(false);
            itemAudioNode.GetComponent<ItemAudioSources>().pickUpBattery.Play(0);
        }
        else if(objData.objType is ObjTypes.pills){
            pillCount += 1;
            obj.SetActive(false);
            itemAudioNode.GetComponent<ItemAudioSources>().pickUpPills.Play(0);
        }
        else if(objData.objType is ObjTypes.paper){
            obj.transform.parent = cameraObj.transform;
            obj.transform.localPosition = new Vector3(0, 0, 0.8f);
            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(-30, 180, 5);
            obj.transform.localRotation = rot;

            holdingObj = obj;
            holdingObj.GetComponent<ObjData>().interactable = false;
            lightObj.GetComponent<LightRotation>().raiseLight = true;

            holdingObj.GetComponent<AudioSource>().Play(0);
        }
    }


    // Update is called once per frame
    void Update()
    {   

        //raycast for item interaction
        RaycastHit hit;
        bool isHit = Physics.Raycast(cameraObj.transform.position, cameraObj.transform.forward, out hit, rayCastDistance);
        if(isHit){
            //Debug.Log(hit.transform.gameObject.name);
            GameObject obj = hit.transform.gameObject;

            ObjData objData = obj.GetComponent<ObjData>();
            if(objData is not null && objData.interactable) {
                if(Input.GetButtonDown("Fire1")){
                    handleObjectPickup(obj, objData);
                }

                //set alt crosshair to active
                setCrosshairAlt(true);
            }
            else {
                //set normal crosshair to active
                setCrosshairAlt(false);
            }
        }
        else {
            //set normal crosshair to active
            setCrosshairAlt(false);
        }


        //put paper away
        if(holdingObj is not null && Input.GetButtonDown("Fire2")){
            holdingObj.SetActive(false);
            inventory.Add(holdingObj);
            holdingObj = null;
            lightObj.GetComponent<LightRotation>().raiseLight = false;
            itemAudioNode.GetComponent<ItemAudioSources>().paper.Play(0);
        }


        //switch flashlight
        if(Input.GetKeyDown("f")) {
            Light light = lightObj.GetComponent<Light>();
            light.enabled = !light.enabled;
            lightOn = !lightOn;

            lightObj.GetComponent<LightAudioSources>().switchLight.Play(0);

        }
        //decrease battery
        if(lightOn)
            battery = Mathf.Max(battery - batteryLossRate * Time.deltaTime, 0);

        //set the state of the light
        if(lightOn && battery > 0)
            lightObj.GetComponent<Light>().enabled = true;
        else
            lightObj.GetComponent<Light>().enabled = false;
        //refill batteries
        if(Input.GetKeyDown("1")) {
            if(batteryCount > 0) {
                battery = maxBattery;
                batteryCount -= 1;
                lightObj.GetComponent<LightAudioSources>().useBattery.Play(0);
            }
            else {
                itemAudioNode.GetComponent<ItemAudioSources>().error.Play(0);
            }
        }


        //decrease sanity from darkness
        if(!lightOn || battery <= 0)
            sanity = Mathf.Max(sanity - sanityLossRate * Time.deltaTime, 0);
        //refill sanity
        if(Input.GetKeyDown("2")) {
            if(pillCount > 0) {
                itemAudioNode.GetComponent<ItemAudioSources>().usePills.Play(0);
                sanity = maxSanity;
                pillCount -= 1;
            }
            else {
                itemAudioNode.GetComponent<ItemAudioSources>().error.Play(0);
            }
        }
        //set the whispers volume based on sanity level
        float cutoffSanity = 70f;
        float maxVolume = 0.8f;
        if(sanity < cutoffSanity)
            whispersAudioNode.GetComponent<AudioSource>().volume = (cutoffSanity - sanity) / cutoffSanity * maxVolume;
        else
            whispersAudioNode.GetComponent<AudioSource>().volume = 0;
    }

}
