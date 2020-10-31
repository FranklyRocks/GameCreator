using UnityEngine;
using Mirror;

public class CharacterMovement : NetworkBehaviour
{
    CharacterController characterController;
    Vector3 playerVelocity;
    bool groundedPlayer;
    float jumpHeight = 1.0f;
    float gravityValue = -9.81f;

    [HideInInspector]
    public Vector3 movementDirection;
    public float movementSpeed = 2;

    [HideInInspector]
    public float rotationDirection;
    public float rotationSpeed = 2;

    public bool jump = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (!isLocalPlayer) enabled = false;
    }

    void FixedUpdate()
    {
        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        characterController.Move(movementDirection * Time.deltaTime * movementSpeed);
        
        //transform.Rotate(0, rotationDirection * rotationSpeed, 0);

        Vector3 camForward = Camera.main.transform.parent.forward;
        camForward.y = 0;
        if(movementDirection != Vector3.zero) transform.forward = Vector3.Slerp(transform.forward, camForward, Time.deltaTime * rotationSpeed);
        //CmdApplyRotate(rotationDirection);

        if (jump && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }
}
