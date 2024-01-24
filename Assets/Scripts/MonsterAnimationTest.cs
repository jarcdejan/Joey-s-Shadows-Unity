using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationTest : MonoBehaviour
{

    public Animator monsterAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("4")) {
            monsterAnimation.SetTrigger("StartWalk");
        }

        if(Input.GetKeyDown("5")) {
            monsterAnimation.SetTrigger("StopWalk");
        }
    }
}
