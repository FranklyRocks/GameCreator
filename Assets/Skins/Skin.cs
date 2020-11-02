using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Skin : NetworkBehaviour
{
    GameObject currentSkin;
    public int skinId;
    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        SetSkin(skinId);
    }

    public override void OnStartLocalPlayer()
    {
        FindObjectOfType<SkinSelector>().localPlayerSkinComponent = this;
        CmdSetSkin(skinId);
    }


    void SetSkin(int id)
    {
        if(!gm) gm = FindObjectOfType<GameManager>();

        // Skin must be different from current skin and must exists
        if (gm.skins[id] == null)
        {
            Debug.Log("Skin " + id + " doesn't exists");
            return;
        }

        Destroy(currentSkin);
        currentSkin = Instantiate(gm.skins[id], transform);
        skinId = id;
    }

    [Command]
    public void CmdSetSkin(int id)
    {
        SetSkin(id);
        RpcSetSkin(id);
    }

    [ClientRpc]
    void RpcSetSkin(int id)
    {
        SetSkin(id);
    }

}
