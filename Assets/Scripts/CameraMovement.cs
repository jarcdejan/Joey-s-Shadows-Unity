using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{   
    public GameObject player;

    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;


    //walking animatiion variables
    public float walkingStepSize = 1.5f;
    public float bobAmplitude = 0.2f;

    private float distance = 0f;
    private Vector3 lastPos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        yRotation = -180f;
    }

    // Update is called once per frame
    void Update()
    {   

        if(player.GetComponent<PlayerLogic>().dead){
            return;
        }
        
        // rotation
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //limit uo/down to 90deg

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);


        //walking animation
        if(player.GetComponent<PlayerMovement>().canMove) {
            distance += (new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(lastPos.x, 0, lastPos.z)).magnitude;
            if(distance > walkingStepSize) {
                distance = distance%(walkingStepSize);
            }

            float yOffset = bobAmplitude * Mathf.Cos(1/walkingStepSize * distance + walkingStepSize/2);
            transform.localPosition = new Vector3(0,yOffset,0);

            lastPos = transform.position;
        }

    }
}
