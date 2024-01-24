using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{

    public AudioSource audioSrc;

    public float playChance = 0.1f;
    public float minInterval = 1f;

    public bool disableRandom = false;

    private float remainingInterval = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(remainingInterval <= 0) {
            float randomNum = Random.Range(0f, 1f);

            if(disableRandom || randomNum < playChance * Time.deltaTime){
                remainingInterval = minInterval;
                audioSrc.Play(0);
            }
        }
        else{
            remainingInterval -= Time.deltaTime;
        }
    }
}
