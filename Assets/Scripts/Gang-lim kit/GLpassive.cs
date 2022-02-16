using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLpassive : GLcontroller
{
    private int basicAttackCount = 0;

    // tracks basic attack cycle to tell if the passive should be proced or not.
    protected bool TrackPassive()
    {
        basicAttackCount += 1;

        if (basicAttackCount == procNumber)
        {
            basicAttackCount = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    // calculates passive proc damage for specific enemy
    protected float CalculatePassiveDamage(Entity enemy)
    {
        return enemy.maxHealth * procMaxHealthPercentageDamage / 100;
    }
}
