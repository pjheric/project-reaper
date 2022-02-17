using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    Entity victimEntity;

    [SerializeField] private GLkit gangLimKit;
    [SerializeField] private Mkit morriganKit;

    private GangLimAttackingHandler GLAttackHandler;
    private TestMorriganAttackingHandler MAttackHandler;

    private void Awake()
    {
        GLAttackHandler = transform.parent.GetComponent<GangLimAttackingHandler>();
        MAttackHandler = transform.parent.GetComponent<TestMorriganAttackingHandler>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        victimEntity = other.GetComponent<Entity>();

        if (gameObject.transform.parent.name == "Gang-Lim")
        {
            if (GLAttackHandler.swinging && victimEntity != null && !victimEntity.isFriendly)
            {
                victimEntity.currentHealth -= gangLimKit.BasicAttackDamage(victimEntity);
                Debug.Log("enemy hit! health: " + victimEntity.currentHealth);

                victimEntity.knockBack(gangLimKit.BasicAttackKnockback(), transform.parent.gameObject);
                victimEntity.hurtColor();

            }
        }
        /*
        else if (gameObject.transform.parent.name == "Morrigan")
        {
            if (MAttackHandler.swinging && victimEntity != null && !victimEntity.isFriendly)
            {
                victimEntity.currentHealth -= morriganKit.BasicAttackDamage(victimEntity);
                Debug.Log("enemy hit! health: " + victimEntity.currentHealth);
                victimEntity.knockBack(morriganKit.BasicAttackKnockback(), transform.parent.gameObject);
            }
        }
        */
    }
}