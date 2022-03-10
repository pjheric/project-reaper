using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mcontroller : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float baseDamage = 3;
    [SerializeField] private float baseKnockback = 0.2f;

    [Header("Passive Stats")]
    [SerializeField] private float bonusAttackSpeedPercentage = 5;
    [SerializeField] private float bonusAttackSpeedPercentageCap = 100;
    [SerializeField] private float passiveRadius = 4;

    [Header("Special Stats")]
    [SerializeField] private float stabbingDelay = 0.375f;
    [SerializeField] private float specialCooldown = 8;
    [SerializeField] private float doomBuffTime = 10;
    [SerializeField] private float specialDamage = 15;
    [SerializeField] private float castingTime = 0.75f;
    [SerializeField] private float doomBuff = 2;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
    

    #region get base stats

    protected float BaseDamage { get { return baseDamage; } }
    public float BaseKnockback { get { return baseKnockback; } }

    #endregion
    #region get passive stats

    protected float GetBonusAttackSpeed { get { return bonusAttackSpeedPercentage; } }
    protected float GetBonusAttackSpeedCap { get { return bonusAttackSpeedPercentageCap; } }
    protected float GetPassiveRadius { get { return passiveRadius; } }

    #endregion
    #region get special stats

    public float GetSpecialCooldown { get{ return specialCooldown; } }
    protected float GetDoomBuffTime { get { return doomBuffTime; } }
    protected float GetSpecialDamage { get{ return specialDamage; } }
    protected float GetCastingTime { get { return castingTime; } }
    protected float GetDoomBuffValue { get { return doomBuff; } }
    protected Transform GetSpecialBorderPt1 { get { return point1; } }
    protected Transform GetSpecialBorderPt2 { get { return point2; } }

    #endregion
}
