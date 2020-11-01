using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour
{
    // Server only script

    public GameObject[] items;

    public UnityEvent Effects;
    GameObject collector;

    public void InvokeEffects(GameObject player)
    {
        collector = player;
        Effects.Invoke();
    } 

    public void Effect_Health(float amount)
    {
        if(collector.TryGetComponent(out Health health)) health.Cure(amount);
    }
}
