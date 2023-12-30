using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float accelarationSize = 20f;
    public float decelarationSize = 30f;
    public float topSpeed = 6f;
    private bool canMove = true;

    private Vector3 velocity = new Vector3(0,0,0);

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
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Calculate movement direction
            Quaternion camRotation = cameraObj.transform.rotation;
            float yaw = Mathf.Atan2(2*camRotation.y*camRotation.w - 2*camRotation.x*camRotation.z, 1 - 2*camRotation.y*camRotation.y - 2*camRotation.z*camRotation.z) * Mathf.Rad2Deg;
            Vector3 movementDirectForward =  (Quaternion.Euler(0, yaw, 0) * Vector3.forward) * verticalInput;
            Vector3 movementDirectRight =  (Quaternion.Euler(0, yaw, 0) * Vector3.right) * horizontalInput;

            Vector3 accelaration = Vector3.Normalize(movementDirectForward + movementDirectRight) * accelarationSize;

            if(accelaration.magnitude == 0) {
                velocity = Vector3.Normalize(velocity) * Mathf.Max(velocity.magnitude - decelarationSize * Time.deltaTime, 0);
            }
            else {
                velocity = velocity + accelaration * Time.deltaTime;
                if(velocity.magnitude > topSpeed)
                    velocity = Vector3.Normalize(velocity) * topSpeed;
            }

            // Move the player
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move(velocity * Time.deltaTime);
            //transform.Translate(movement);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit col)
    {
        //Debug.Log(col.collider.gameObject);
    }
}
