using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    Entity victimEntity;

    // STATS
    [SerializeField] float damage = 3;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("AWDAWDawd");
        victimEntity = other.GetComponent<Entity>();

        victimEntity.currentHealth -= damage;

        Debug.Log("enemy hit! health: " + victimEntity.currentHealth);
    }
}