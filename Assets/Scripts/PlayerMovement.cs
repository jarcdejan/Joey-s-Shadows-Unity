using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 5f;
    private bool canMove = true;

    public GameObject cameraObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // Get input for movement
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate movement direction
            Quaternion camRotation = cameraObj.transform.rotation;
            float yaw = Mathf.Atan2(2*camRotation.y*camRotation.w - 2*camRotation.x*camRotation.z, 1 - 2*camRotation.y*camRotation.y - 2*camRotation.z*camRotation.z) * Mathf.Rad2Deg;
            Vector3 movementDirectForward =  (Quaternion.Euler(0, yaw, 0) * Vector3.forward) * verticalInput;
            Vector3 movementDirectRight =  (Quaternion.Euler(0, yaw, 0) * Vector3.right) * horizontalInput;

            Vector3 movement = Vector3.Normalize(movementDirectForward + movementDirectRight) * moveSpeed * Time.deltaTime;

            // Move the player
            transform.Translate(movement);
        }
    }
}
