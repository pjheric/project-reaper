using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLbasicattack : GLpassive
{
    // returns autoattack damage.
    public float ManageBasicAttack(Entity enemy)
    {
        if (TrackPassive())
        {
            return BaseDamage() + CalculatePassiveDamage(enemy);
        }
        else
        {
            return BaseDamage();
        }
    }
}
