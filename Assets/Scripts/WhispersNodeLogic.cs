using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhispersNodeLogic : MonoBehaviour
{   

    public GameObject playerObj;

    public float cutoffSanity = 70f;
    public float maxVolume = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set the whispers volume based on sanity level
        float sanity = playerObj.GetComponent<PlayerLogic>().sanity;
        float volume = GetComponent<AudioSource>().volume;
        float targetVolume;

        if(sanity < cutoffSanity)
            targetVolume = (cutoffSanity - sanity) / cutoffSanity * maxVolume;
        else
            targetVolume = 0;

        GetComponent<AudioSource>().volume = volume + (targetVolume - volume) * Time.deltaTime;
    }
}
