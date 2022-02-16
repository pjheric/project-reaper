using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    Entity victimEntity;

    [SerializeField] private GLkit gangLimKit;

    private AttackingHandler playerAttackHandler;


    private void Awake()
    {
        playerAttackHandler = transform.parent.GetComponent<AttackingHandler>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        victimEntity = other.GetComponent<Entity>();

        // checks if it is an entity (weapons) and is not friendly
        if (playerAttackHandler.swinging && victimEntity != null && !victimEntity.isFriendly)
        {
            if (gameObject.transform.parent.name == "Gang-Lim")
            {
                victimEntity.currentHealth -= gangLimKit.BasicAttackDamage(victimEntity);
                Debug.Log("enemy hit! health: " + victimEntity.currentHealth);

                victimEntity.knockBack(gangLimKit.BasicAttackKnockback(), transform.parent.gameObject);
            }
            /*
            else if (gameObject.transform.parent.name == "morigan")
            {

            }
            */
        }
    }
}