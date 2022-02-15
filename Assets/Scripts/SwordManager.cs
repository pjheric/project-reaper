using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    Entity victimEntity;

    // STATS
    [SerializeField] float damage = 3;

    private AttackingHandler playerAttackHandler;


    private void Awake()
    {
        playerAttackHandler = transform.parent.GetComponent<AttackingHandler>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        victimEntity = other.GetComponent<Entity>();

        if (playerAttackHandler.swinging)
        {
            // checks if it is an entity (weapons) and is not friendly
            if (victimEntity != null && !victimEntity.isFriendly)
            {
                victimEntity.currentHealth -= damage;
                Debug.Log("enemy hit! health: " + victimEntity.currentHealth);

            }
            
        }
        


    }
}