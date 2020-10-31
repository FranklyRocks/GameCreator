using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Item : NetworkBehaviour
{
    public enum itemType
    {
        Weapon,
        Food
    }

    public itemType type;

    public virtual void Action() { }

}
