using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    Entity victimEntity;

    // STATS
    [SerializeField] float damage = 3;
    [SerializeField] float knockback = 0.2f;

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
            victimEntity.currentHealth -= damage;
            Debug.Log("enemy hit! health: " + victimEntity.currentHealth);

            victimEntity.knockBack(knockback, transform.parent.gameObject);
        }
    }
}