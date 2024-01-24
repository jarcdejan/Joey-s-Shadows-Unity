using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{

    public GameObject lightObj;
    public GameObject cameraObj;

    public float rayCastDistance = 10f;
    public bool lookingAtIntractableObj = false;

    public int batteryCount = 0;
    public int pillCount = 0;
    public int keyCount = 0;

    public float maxSanity = 100f;
    public float sanity = 100f;
    public float sanityLossRate = 1;

    public GameObject itemAudioNode;

    public bool dead = false;
    public float lightDropDelay = 1f;

    private GameObject holdingObj;
    private List<GameObject> inventory = new List<GameObject>();

    private float lightDropCountdown;

    public float deathTimer = 4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void handleObjectInteracion(GameObject obj, ObjData objData)
    {

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

            if(holdingObj is not null)
                return;
            
            obj.transform.parent = cameraObj.transform;
            obj.transform.localPosition = new Vector3(0, 0, 0.3f);
            Quaternion rot = new Quaternion();
            rot.eulerAngles = new Vector3(-20, 180, 5);
            obj.transform.localRotation = rot;

            holdingObj = obj;
            holdingObj.GetComponent<ObjData>().interactable = false;
            lightObj.GetComponent<LightRotation>().raiseLight = true;

            holdingObj.GetComponent<AudioSource>().Play(0);
        }
        else if(objData.objType is ObjTypes.door){
            bool hasKey = keyCount > 0;
            obj.GetComponent<DoorLogic>().Interact(hasKey);
        }
        else if(objData.objType is ObjTypes.exitDoor){
            if(keyCount > 0) {
                obj.GetComponent<Animation>().Play("exitDoorOpen");
                obj.GetComponent<ExitDoorAudioNode>().doorUnlock.Play(0);
                obj.GetComponent<ObjData>().interactable = false;
            }
            else if(!obj.GetComponent<Animation>().isPlaying){
                obj.GetComponent<Animation>().Play("exitDoorLocked");
                obj.GetComponent<ExitDoorAudioNode>().doorLocked.Play(0);
            }
        }
        else if(objData.objType is ObjTypes.key){
            keyCount += 1;
            obj.SetActive(false);
            itemAudioNode.GetComponent<ItemAudioSources>().pickUpKeys.Play(0);
        }
    }


    //kill player
    public void Die() {
        if(dead)
            return;

        dead = true;
        GetComponent<CharacterController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Animation>().Play("DeathAnimation");
        GetComponent<AudioSource>().PlayDelayed(1.9f);
        lightDropCountdown = lightDropDelay;
    }

    public void Win() {
        Cursor.visible = true; // Set cursor to visible
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("winSreen");
    }


    // Update is called once per frame
    void Update()
    {   

        if(dead) {
            if(lightDropDelay < 0) {
                lightObj.GetComponent<MeshCollider>().enabled = true;
                lightObj.GetComponent<Rigidbody>().isKinematic = false;
                lightObj.GetComponent<Rigidbody>().useGravity = true;
            }
            else
                lightDropDelay -= Time.deltaTime;

            if(deathTimer < 0){
                Cursor.visible = true; // Set cursor to visible
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("endScreen");
            }
            else
                deathTimer -= Time.deltaTime;

            return;
        }

        if(sanity <= 0){
            Die();
        }

        //put object away
        if(holdingObj is not null && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))){
            holdingObj.SetActive(false);
            inventory.Add(holdingObj);
            holdingObj = null;
            lightObj.GetComponent<LightRotation>().raiseLight = false;
            itemAudioNode.GetComponent<ItemAudioSources>().paper.Play(0);
        }

        //raycast for item interaction
        RaycastHit hit;
        bool isHit = Physics.Raycast(cameraObj.transform.position, cameraObj.transform.forward, out hit, rayCastDistance);
        if(isHit){
            //Debug.Log(hit.transform.gameObject.name);
            GameObject obj = hit.transform.gameObject;

            ObjData objData = obj.GetComponent<ObjData>();

            if(objData is null && obj.transform.parent is not null) {
                obj = obj.transform.parent.gameObject;
                objData = obj.GetComponent<ObjData>();
            }

            if(objData is not null && objData.interactable) {
                if(Input.GetButtonDown("Fire1")){
                    handleObjectInteracion(obj, objData);
                }

                lookingAtIntractableObj = true;
            }
            else {
                lookingAtIntractableObj = false;
            }
        }
        else {
            lookingAtIntractableObj = false;
        }


        LightLogic light = lightObj.GetComponent<LightLogic>();
        //switch flashlight
        if(Input.GetKeyDown("f")) {
            light.lightOn = !light.lightOn;

            lightObj.GetComponent<LightAudioSources>().switchLight.Play(0);
        }
        //refill batteries
        if(Input.GetKeyDown("1")) {
            if(batteryCount > 0) {
                light.battery = light.maxBattery;
                batteryCount -= 1;
                lightObj.GetComponent<LightAudioSources>().useBattery.Play(0);
            }
            else {
                itemAudioNode.GetComponent<ItemAudioSources>().error.Play(0);
            }
        }

        //decrease sanity from darkness
        if(!light.lightOn || light.battery <= 0)
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

    }

}
