using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{

    public GameObject playerObj;
    public Animator monsterAnimation;

    public float speed = 1f;
    public float stopDistance = 2f;

    public float TPChance = 0.5f;
    public float maxTPDistance = 2f;
    public float tpDelay = 0.01f;
    private float tpTimer = 0f;
    private bool teleporting = false;

    public float tpCooldown = 1f;
    private float tpCooldownRemaining = 0f;

    public float idleToWalkTime = 1f;
    public float walkToIdleTime = 1f;
    public float killTime = 2f;

    private string state;

    public void StartEvent(GameObject player, float tpDistance) {
        playerObj = player;
        maxTPDistance = tpDistance;
        monsterAnimation.SetTrigger("StartWalk");
        state = "idleToWalk";
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        //face player
        Vector3 toPlayer = Vector3.Normalize(playerObj.transform.position - transform.position);
        toPlayer.y = 0;
        Quaternion rot = Quaternion.LookRotation(toPlayer, Vector3.up);
        float yaw = Mathf.Atan2(2*rot.y*rot.w - 2*rot.x*rot.z, 1 - 2*rot.y*rot.y - 2*rot.z*rot.z) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(0, yaw, 0);

        //teleport
        if(teleporting) {
            tpTimer += Time.deltaTime;
            if(tpTimer >= tpDelay) {
                float newX = Random.Range(-maxTPDistance, maxTPDistance);
                transform.localPosition = new Vector3(newX, transform.localPosition.y, transform.localPosition.z);
                teleporting = false;
            }
        }
        if(tpCooldownRemaining > 0) {
            tpCooldownRemaining -= Time.deltaTime;
        }


        switch(state) {
            case "idleToWalk":

                //wait for idle to walk animation
                if(idleToWalkTime > 0) {
                    idleToWalkTime -= Time.deltaTime;
                }
                else {
                    state = "walking";
                    Debug.Log(state);
                }
                break;

            case "walking":

                float dist = (playerObj.transform.position - transform.position).magnitude;

                if(dist > stopDistance) {

                    //move toward player
                    transform.position = transform.position + toPlayer * speed * Time.deltaTime;

                    //random teleportation
                    if(tpCooldownRemaining <= 0){
                        float randomNum = Random.Range(0f, 1f);
                        if(randomNum < TPChance * Time.deltaTime){
                            GetComponent<MonsterAudioNode>().whoosh.Play(0);
                            transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                            tpTimer = 0f;
                            teleporting = true;
                            tpCooldownRemaining = tpCooldown;
                        }
                    }
                }
                else {
                    monsterAnimation.SetTrigger("StopWalk");
                    state = "stopping";
                    Debug.Log(state);
                }

                break;

            case "stopping":

                if(monsterAnimation.GetCurrentAnimatorStateInfo(0).IsName("WalkToIdle")) {
                    state = "walkToIdle";
                    Debug.Log(state);
                    GetComponent<MonsterAudioNode>().scream.Play(0);
                }
                else {
                    //move toward player
                    transform.position = transform.position + toPlayer * speed * Time.deltaTime;
                }

                break;

            case "walkToIdle":

                if(walkToIdleTime > 0) {
                    //move toward player
                    transform.position = transform.position + toPlayer * speed * Time.deltaTime;

                    walkToIdleTime -= Time.deltaTime;
                }
                else {
                    state = "attack";
                    Debug.Log(state);
                }
                break;
            
            case "attack":

                if(killTime > 0) {
                    killTime -= Time.deltaTime;
                }
                else {
                    playerObj.GetComponent<PlayerLogic>().Die();
                    playerObj.GetComponent<PlayerLogic>().sanity = 0;
                    state = "end";
                    Debug.Log(state);
                }
                break;

            default: break;
        }

    }
}
