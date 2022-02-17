using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mkit : MonoBehaviour
{
    private Mbasicattack basicAttack;

    private void Awake()
    {
        basicAttack = GetComponent<Mbasicattack>();
    }

    public float BasicAttackDamage(Entity enemy)
    {
        return basicAttack.ManageBasicAttack(enemy);
    }

    public float BasicAttackKnockback()
    {
        return basicAttack.BaseKnockback();
    }
}
