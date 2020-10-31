using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public float damage = 10;
    [SyncVar]
    public float ammo = 10;
    public float range = 10;

    public override void Action()
    {
        Shoot();
    }

    private void Update()
    {
        // Set for owner only(localplayer)

        //if (!NetworkClient.isLocalClient) return;
        //transform.forward = Camera.main.transform.forward;
    }

    void Shoot()
    {
        if (ammo == 0) return;
        CmdShoot(Camera.main.transform.position, Camera.main.transform.forward);
    }

    [Command]
    void CmdShoot(Vector3 position, Vector3 forward)
    {
        if (ammo == 0) return;
        if (Physics.Raycast(position, forward, out RaycastHit hit, range))
        {
            if (hit.transform.TryGetComponent(out Health health))
            {
                health.Damage(damage);
            }
        }
        ammo--;
    }
}
