using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErraticEnemy : MonoBehaviour
{
    [SerializeField]
    Entity entity;
    [SerializeField]
    Enemy enemy;

    [SerializeField]
    float normalSpeed;
    [SerializeField]
    float acceleratedSpeed;
    
    [SerializeField]
    float minZigDistance;
    [SerializeField]
    float maxZigDistance;
    [SerializeField]
    float zigForwardAngle;
    [SerializeField]
    float zigAngleVariation;
    [SerializeField]
    float zigTime;
    [SerializeField]
    float chargeCoolDownTime;

    float currentChargeCoolDownTime;
    private float currentZigTime;
    private Vector2 zigDir;
    private bool zigLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // erraticMoveDecide();
        act(enemy.locus);
    }

    void OnDestroy()
    {
        if(gameObject.scene.isLoaded) //Was Deleted
        {
            waveManager.enemyCounts[(int)enemyType.erratic] -=1;
            waveManager.enemyCount -= 1;
        }
    }
    public void erraticMove()
    {
        enemy.moveDir(zigDir);
    }
    //will choose a position at a radius similair to its current position +- min zig max zig distance, alternating + and -
    //will choose an angle further along the circle at slightly random angle
    public void erraticMoveDecide()
    {
        zigLeft = !zigLeft;
        float currentAngle = (transform.position.y >= 0) ? Vector2.Angle(Vector2.right, transform.position) : Vector2.Angle(Vector2.right, transform.position) * -1;
        float newAngle = currentAngle + zigForwardAngle + Random.Range(-zigAngleVariation, zigAngleVariation);
        float newRadius = (zigLeft) ? (Vector2.Distance(Vector2.zero,transform.position) + Random.Range(minZigDistance,maxZigDistance)) : (Vector2.Distance(Vector2.zero,transform.position) - Random.Range(minZigDistance,maxZigDistance));
        newRadius = (newRadius < 10) ? 10 : newRadius;
        newRadius = (newRadius > 90) ? 90 : newRadius;
        Vector2 zigSpot = new Vector2(newRadius * Mathf.Cos(Mathf.Deg2Rad * newAngle), newRadius * Mathf.Sin(Mathf.Deg2Rad * newAngle));
        zigDir = zigSpot - (Vector2)transform.position;
        zigDir.Normalize();
    }
    public void act(GameObject locus)//called every update
    {
        currentZigTime += Time.deltaTime;
        currentChargeCoolDownTime += Time.deltaTime;
        enemy.checkDeath();
        //enemy.CheckIfPlayerAttackable();
        if (currentChargeCoolDownTime > chargeCoolDownTime)//as long as its currentcharge is greater than charge cooldown it will do the charge stuff
        {
            if(enemy.isAttacking)//then it has done the attack and should stop charge
            {
                StopCharge();
            }
            else
            {
                charge();
            }
        }
        else{
            if(currentZigTime > zigTime)
            {
                erraticMoveDecide();
                currentZigTime = 0.0f;
            }   
            erraticMove();  
        } 
    }

    public void StopCharge()
    {
        entity.maxSpeed = normalSpeed;
        currentChargeCoolDownTime = 0;
    }

    public void charge()
    {
        entity.maxSpeed = acceleratedSpeed;
        enemy.moveandAttackNearestPlayer();
    }
}
