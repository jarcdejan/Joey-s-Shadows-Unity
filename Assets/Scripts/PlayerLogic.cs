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

    private GameObject holdingObj;
    List<GameObject> inventory = new List<GameObject>();

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
        }
        else if(objData.objType is ObjTypes.pills){
            pillCount += 1;
            obj.SetActive(false);
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
        }
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


        //drop object
        if(holdingObj is not null && Input.GetButtonDown("Fire2")){
            holdingObj.SetActive(false);
            inventory.Add(holdingObj);
            holdingObj = null;
            lightObj.GetComponent<LightRotation>().raiseLight = false;
        }
    }

}
