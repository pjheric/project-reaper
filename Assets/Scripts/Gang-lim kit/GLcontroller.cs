using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLcontroller : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float baseDamage = 3;
    [SerializeField] private float baseKnockback = 0.2f;

    [Header("Passive")]
    [SerializeField] protected int procNumber = 3;
    [SerializeField] protected float procMaxHealthPercentageDamage = 5;

    protected float BaseDamage()
    {
        return baseDamage;
    }

    public float BaseKnockback()
    {
        return baseKnockback;
    }
}
