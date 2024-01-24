using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepLogic : MonoBehaviour
{   
    public float stepSize = 1f;

    public AudioSource step1;
    public AudioSource step2;
    public AudioSource step3;
    public AudioSource step4;

    private float distance = 0f;
    private Vector3 lastPos;

    private int currAudioSource = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   

        if(!transform.parent.GetComponent<PlayerMovement>().canMove) {
            return;
        }

        distance += (new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(lastPos.x, 0, lastPos.z)).magnitude;

        if(distance >= stepSize) {
            distance = distance%stepSize;

            switch(currAudioSource) 
            {
                case 0: step1.Play(0); break;
                case 1: step2.Play(0); break;
                case 2: step3.Play(0); break;
                case 3: step4.Play(0); break;
                default: break;
            }

            currAudioSource = (currAudioSource + 1) % 4;           
        }

        lastPos = transform.position;
    }
}
