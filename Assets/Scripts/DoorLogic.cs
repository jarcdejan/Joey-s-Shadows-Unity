using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{   
    public AudioSource doorOpen;
    public AudioSource doorLocked;
    public AudioSource doorUnlock;
    public AudioSource doorClose;

    public Animation doorAnimation;

    public bool locked = false;
    
    private bool open = false;


    public void Interact(bool hasKey) {

        if(GetComponent<Animation>().isPlaying) {
            return;
        }

        if(open) {
            doorAnimation.Play("DoorClose");
            doorClose.Play(0);
            open = false;
        }
        else {
            if(locked) {
                if(hasKey) {
                    doorAnimation.Play("DoorOpen");
                    doorUnlock.Play(0);
                    open = true;
                    locked = false;
                }
                else {
                    doorAnimation.Play("DoorLocked");
                    doorLocked.Play(0);
                }
            }
            else {
                doorAnimation.Play("DoorOpen");
                doorOpen.Play(0);
                open = true;
            }
        }
    }
}
