using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerStateManager : NetworkBehaviour
{
    public enum states {
        Walking,
        Driving
    }

    [HideInInspector] public states currentState = states.Walking;

    public void HandleState()
    {
        if (currentState == states.Driving)
        {
            CmdExitCar();
        }
        else
        {
            CmdAction();
        }
    }

    [Command]
    void CmdAction()
    {
        RaycastHit[] collisions = Physics.SphereCastAll(transform.position, 1.25f, transform.forward, 1.25f);

        foreach(RaycastHit col in collisions)
        {
            // Use return to prevent effect more than one object per CmdAction() call
            switch(col.transform.tag)
            {
                case "Car":
                    EnterCar(col.transform.gameObject);
                    return;
                case "Item":
                    GetComponent<Inventory>().AddItem(col.transform.GetComponent<Pickup>().pickupPrefab);
                    return;
            }
        }
    }

    GameObject currentCar;

    void EnterCar(GameObject car)
    {
        car.GetComponent<Seat>().SetDriver(gameObject);
        GetComponent<CharacterController>().enabled = false;

        // Sit player on seat
        gameObject.transform.SetParent(car.GetComponent<Seat>().seat.transform);
        transform.localPosition = Vector3.zero;
        transform.forward = car.transform.forward;


        // Assing authority, so the player can use the car
        car.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);

        RpcEnterCar(car);
        currentState = states.Driving;
        currentCar = car;

    }

    [ClientRpc]
    void RpcEnterCar(GameObject car)
    {
        currentCar = car;

        currentCar.GetComponent<Seat>().SetDriver(gameObject);
        GetComponent<CharacterController>().enabled = false;


        // Sit player on seat
        gameObject.transform.SetParent(currentCar.GetComponent<Seat>().seat.transform);
        transform.localPosition = Vector3.zero;
        transform.forward = currentCar.transform.forward;

        currentState = states.Driving;
    }
    
    [Command]
    void CmdExitCar()
    {
        currentCar.GetComponent<Seat>().RemoveDriver();
        GetComponent<CharacterController>().enabled = true;

        currentCar.GetComponent<NetworkIdentity>().RemoveClientAuthority();
        //currentCar = null;

        gameObject.transform.SetParent(null);
        currentState = states.Walking;

        RpcExitCar();
    }

    [ClientRpc]
    void RpcExitCar()
    {
        if(!currentCar) currentCar.GetComponent<Seat>().RemoveDriver();
        GetComponent<CharacterController>().enabled = true;

        currentCar = null;
        gameObject.transform.SetParent(null);
        currentState = states.Walking;
    }
}
