using UnityEngine;
using Mirror;

public class PlayerInput : NetworkBehaviour
{
    CharacterMovement characterMovement;
    PlayerStateManager playerStateManager;
    Inventory inventory;

    public float vertical;
    public float horizontal;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        playerStateManager = GetComponent<PlayerStateManager>();
        inventory = GetComponent<Inventory>();

        if (!isLocalPlayer) enabled = false;

    }

    public override void OnStartLocalPlayer()
    {
        CameraOrbit camConfig = FindObjectOfType<CameraOrbit>();
        camConfig.target = transform;

        /*
        FollowAndOrbit camConfig = Camera.main.GetComponent<FollowAndOrbit>();
        camConfig.target = transform;
        camConfig.parentRigidbody = GetComponent<CharacterController>();
        camConfig.enabled = true;
        */
    }

    void Update()
    {

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if(playerStateManager.currentState == PlayerStateManager.states.Walking)
        {
            characterMovement.movementDirection = vertical * Camera.main.transform.parent.forward + horizontal * Camera.main.transform.parent.right;
            characterMovement.rotationDirection = horizontal;
            characterMovement.jump = Input.GetButtonDown("Jump");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerStateManager.HandleState();
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            // If player if driving do not execute inventory actions
            if (playerStateManager.currentState == PlayerStateManager.states.Driving) return;
            if (inventory.items.Count == 0 || !inventory.items[inventory.selectedItem]) return;
            inventory.items[inventory.selectedItem].GetComponent<Item>().Action();
        }
    }
}
