using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mcontroller : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] private float baseDamage = 3;
    [SerializeField] private float baseKnockback = 0.2f;

    //PASSIVE on hold

    protected float BaseDamage()
    {
        return baseDamage;
    }

    public float BaseKnockback()
    {
        return baseKnockback;
    }
}
