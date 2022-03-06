using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mkit : MonoBehaviour
{
    private Mbasicattack basicAttack;
    private Mpassive passive;

    private void Awake()
    {
        basicAttack = GetComponent<Mbasicattack>();
        passive = GetComponent<Mpassive>();
    }

    public float BasicAttackDamage { get{ return basicAttack.ManageBasicAttack(); } }
    public float BasicAttackKnockback { get { return basicAttack.BaseKnockback; } }
    public float GetPassiveBonusAttackSpeed { get { return passive.GetPassiveBonusAttackSpeed(); } }

    public void TrackPassive()
    {
        passive.ManagePassive();
    }
}
