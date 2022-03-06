using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLkit : MonoBehaviour
{
    private GLbasicattack basicAttack;

    private void Awake()
    {
        basicAttack = GetComponent<GLbasicattack>();
    }

    public float BasicAttackDamage(Entity enemy)
    {
        return basicAttack.ManageBasicAttack(enemy);
    }

    public float BasicAttackKnockback()
    {
        return basicAttack.BaseKnockback;
    }

    public bool CanBasicAttack { get { return basicAttack; } }
}
