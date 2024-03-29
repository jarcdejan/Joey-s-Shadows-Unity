using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float accelarationSize = 20f;
    public float decelarationSize = 30f;
    public float topSpeed = 6f;

    public float gravity = 40f;
    public float terminalVelocity = 10f;

    public bool canMove = true;

    private Vector3 velocity = new Vector3(0,0,0);
    private Vector3 verticalVelocity = new Vector3(0,0,0);

    public GameObject cameraObj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   

        if(GetComponent<PlayerLogic>().dead){
            return;
        }

        //calculate horizontal rotation from camera rotation
        Quaternion camRotation = cameraObj.transform.rotation;
        float yaw = Mathf.Atan2(2*camRotation.y*camRotation.w - 2*camRotation.x*camRotation.z, 1 - 2*camRotation.y*camRotation.y - 2*camRotation.z*camRotation.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, yaw, 0);

        if (canMove)
        {
            // Get input for movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            // Calculate accelaration direction
            Vector3 movementDirectForward =  (Quaternion.Euler(0, yaw, 0) * Vector3.forward) * verticalInput;
            Vector3 movementDirectRight =  (Quaternion.Euler(0, yaw, 0) * Vector3.right) * horizontalInput;

            Vector3 accelaration = Vector3.Normalize(movementDirectForward + movementDirectRight) * accelarationSize;

            //add accelaration or decelaration to velocity
            if(accelaration.magnitude == 0) {
                velocity = Vector3.Normalize(velocity) * Mathf.Max(velocity.magnitude - decelarationSize * Time.deltaTime, 0);
            }
            else {
                velocity = velocity + accelaration * Time.deltaTime;
                if(velocity.magnitude > topSpeed)
                    velocity = Vector3.Normalize(velocity) * topSpeed;
            }

            //compute vertical velocity
            verticalVelocity = verticalVelocity + new Vector3(0, gravity, 0) * Time.deltaTime;
            if(verticalVelocity.magnitude > terminalVelocity)
                    verticalVelocity = Vector3.Normalize(verticalVelocity) * terminalVelocity;

            // Move the player
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move((velocity + verticalVelocity) * Time.deltaTime);
            //transform.Translate(movement);
        }
        else{
            velocity = new Vector3(0,0,0);
        }
    }

}
