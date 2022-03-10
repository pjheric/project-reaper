using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLcontroller : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float baseDamage = 3;
    [SerializeField] private float baseKnockback = 0.2f;
    [SerializeField] private bool canBasickAttack = true;

    [Header("Passive")]
    [SerializeField] protected int procNumber = 3;
    [SerializeField] protected float procMaxHealthPercentageDamage = 5;

    [Header("Special Stats")]
    [SerializeField] private float specialCooldown = 8;
    [SerializeField] private float specialDuration = 6;
    [SerializeField] private float specialRadius = 3;
    [SerializeField] private float specialMoveSpeedPercentage = 30;
    [SerializeField] private float specialDPS = 8;

    #region get base stats
    protected float BaseDamage { get { return baseDamage; } }
    public float BaseKnockback { get { return baseKnockback; } }
    public bool CanBasicAttack { get { return canBasickAttack; } set { canBasickAttack = value; } }
    #endregion
    #region get passive stats
    protected float GetProcNumber { get { return procNumber; } }
    protected float GetProcDamagePercentage { get { return procMaxHealthPercentageDamage; } }
    #endregion
    #region get special stats
    public float GetSpecialCooldown { get { return specialCooldown; } }
    protected float GetSpecialDuration { get { return specialDuration; } }
    protected float GetSpecialRadius { get { return specialRadius; } }
    protected float GetSpecialMoveSpeedPercentage { get { return specialMoveSpeedPercentage; } }
    protected float GetSpecialDPS { get { return specialDPS; } }
    #endregion
}
