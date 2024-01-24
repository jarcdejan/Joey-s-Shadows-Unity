using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEvent : MonoBehaviour
{   

    public GameObject cameraObj;
    private GameObject monsterObj;
    private GameObject monsterEventObj;

    public GameObject crosshairAlt2;

    public bool eventActive = false;
    public bool lookingAtMonster = false;

    public float sanityLossRate = 2f;

    public float maxProgress = 20f;
    public float progress = 0f;
    public string nextKey;

    public float shakeRadius = 0.02f;
    private Vector3 originalPosition;

    public void StartEvent(GameObject monster, GameObject monsterEvent) {
        monsterObj = monster;
        monsterEventObj = monsterEvent;
        eventActive = true;
        progress = 0;
        nextKey = "q";
        GetComponent<PlayerMovement>().canMove = false;
        originalPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CheckForClick() {
        if(nextKey.Equals("q")){
            if(Input.GetButtonDown("Q")){
                progress += 1;
                nextKey = "e";
            }
        }
        else if(nextKey.Equals("e")) {
            if(Input.GetButtonDown("E")){
                progress += 1;
                nextKey = "q";
            }
        }
    }


    // Update is called once per frame
    void Update()
    {   
        if(!eventActive) {
            return;
        }

        RaycastHit hit;
        bool isHit = Physics.Raycast(cameraObj.transform.position, cameraObj.transform.forward, out hit);
        if(isHit){
            //Debug.Log(hit.transform.gameObject.name);
            GameObject obj = hit.transform.gameObject;

            if(obj == monsterObj) {
                lookingAtMonster = true;
                CheckForClick();
            }
            else {
                lookingAtMonster = false;
            }
        }
        else {
            lookingAtMonster = false;
        }


        //check for progress
        if(progress < maxProgress) {
            GetComponent<PlayerLogic>().sanity -= sanityLossRate * Time.deltaTime;
        }
        else {
            eventActive = false;
            GetComponent<PlayerMovement>().canMove = true;
            lookingAtMonster = false;
            monsterObj.SetActive(false);
            monsterEventObj.GetComponent<AudioSource>().Play(0);
            progress = 0;
            transform.position = originalPosition;
        }

        //move the player around to create the illusion of shaking
        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);
        
        transform.position = originalPosition + new Vector3(randomX * shakeRadius, 0, randomZ * shakeRadius);
    }
}
