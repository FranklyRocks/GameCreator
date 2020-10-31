using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Seat : NetworkBehaviour
{
    PlayerInput driver;
    public GameObject seat;
    CarController car;

    void Start()
    {
        car = GetComponent<CarController>();
        enabled = false;
    }

    void Update()
    {
        car.torque = driver.vertical;
        car.steer = driver.horizontal;
    }

    public void SetDriver(GameObject player)
    {
        driver = player.GetComponent<PlayerInput>();
        enabled = true;
    }

    public void RemoveDriver()
    {
        driver = null;
        enabled = false;
    }
}
