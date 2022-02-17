using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mbasicattack : Mpassive
{
    // returns autoattack damage.
    public float ManageBasicAttack(Entity enemy)
    {
        // would also have passive here
        return BaseDamage();
    }
}
