using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SkinSelector : NetworkBehaviour
{
    public Skin localPlayerSkinComponent;

    public void SetSkin(int id)
    {
        localPlayerSkinComponent.CmdSetSkin(id);
    }
}
