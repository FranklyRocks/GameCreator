using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : NetworkBehaviour
{
    public List<GameObject> items = new List<GameObject>();

    public int selectedItem;

    [Server]
    public void AddItem(GameObject prefab)
    {
        GameObject item = Instantiate(prefab, transform);
        NetworkServer.Spawn(item, gameObject);

        GetComponent<NetworkTransformChild>().enabled = true;
        GetComponent<NetworkTransformChild>().target = item.transform;

        item.transform.localPosition = Vector3.zero + Vector3.right * 0.5f;
        items.Add(item);
        RpcAddItem(item);
    }

    [ClientRpc]
    void RpcAddItem(GameObject item)
    {
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero + Vector3.right * 0.5f;
        items.Add(item);

        GetComponent<NetworkTransformChild>().enabled = true;
        GetComponent<NetworkTransformChild>().target = item.transform;
    }
}
