using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
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
    public void act(GameObject locus)//called every update
    {
        enemy.moveTowardsLocus(locus);
    }
}
