using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kit : MonoBehaviour
{
    public virtual float BasicAttackDamage(Entity enemy)
    {
        return 0;
    }

    public virtual float BasicAttackKnockback()
    {
        return 0;
    }
}
