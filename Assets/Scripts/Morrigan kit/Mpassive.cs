using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mpassive : Mcontroller
{
    private int numberOfEnemies = 0;
    private float specialDouble = 1;

    public void ManagePassive()
    {
        numberOfEnemies = 0;
        Collider2D[] allHitColliders = Physics2D.OverlapCircleAll(transform.position, GetPassiveRadius);

        foreach (var x in allHitColliders)
        {
            if (x.transform.tag == "enemy")
            {
                numberOfEnemies += 1;
            }
        }
    }

    public float GetPassiveBonusAttackSpeed()
    {
        if (GetBonusAttackSpeed * numberOfEnemies >= GetBonusAttackSpeedCap)
        {
            return GetBonusAttackSpeedCap * specialDouble;
        }

        return GetBonusAttackSpeed * numberOfEnemies * specialDouble;
    }

    protected void ActivateDoomEffect()
    {
        specialDouble = 2;
    }

    protected void DeactivateDoomEffect()
    {
        specialDouble = 1;
    }
}
