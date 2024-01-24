using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMonsterEvent : MonoBehaviour
{
    public GameObject monsterObj;
    public GameObject playerObj;

    public float maxTPDistance = 2f;
    public bool isEnabled = true;

    private void OnTriggerEnter(Collider other)
    {   
        if(isEnabled && other.gameObject == playerObj) {
            GameObject localMonster = Instantiate(monsterObj, transform.parent);
            localMonster.transform.localPosition = new Vector3(0,0,0);
            localMonster.SetActive(true);
            localMonster.GetComponent<MonsterMovement>().StartEvent(playerObj, maxTPDistance);
            playerObj.GetComponent<MonsterEvent>().StartEvent(localMonster, transform.parent.gameObject);
            localMonster.GetComponent<MonsterAudioNode>().jumpscare.Play(0);
            isEnabled = false;
        }
    }
}
