using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    [SerializeField]
    Entity entity;
    [SerializeField]
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        act(enemy.locus);
    }

    void OnDestroy()
    {
        if(gameObject.scene.isLoaded) //Was Deleted
        {
            waveManager.enemyCounts[(int)enemyType.tank] -=1;
            waveManager.enemyCount -= 1;
        }
    }
    public void act(GameObject locus)//called every update
    {
        enemy.checkDeath();
        enemy.CheckIfPlayerAttackable();
        enemy.moveAndAttack(locus);
    }
}
