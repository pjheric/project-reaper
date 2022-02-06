using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    static int enemyCount;

    float health;
    GameObject me;
    public Enemy()
    {

    }
//the base class
//will have health, basic movement, etc
//lots of behaviour like dashing and auras and attacking
//the subclasses will each have specific functions that bring together components from the base class into the main functions that will abe called by enemy like (move, attack, special, etc)
    protected void moveTowardsLocus(GameObject locus)
    {

    }
    protected void moveTowardsNearestPlayer()
    {

    }
    protected void attackBasic()
    {

    }
}
public class BasicEnemy : Enemy
{
    void move(GameObject target)//called every update
    {
        moveTowardsLocus(target);
    }
    void attack(GameObject target)
    {

    }

}