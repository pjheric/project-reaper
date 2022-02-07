using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Entity entity;

    static int enemyCount;

    public float attackRange;
    public float attackDamage;

    public GameObject locus;//will need to be set when it spawns

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void moveTowardsLocus(GameObject locus)
    {
        if (Vector2.Distance(locus.transform.position, transform.position)> attackRange + locus.GetComponent<CircleCollider2D>().radius)
        {
            move(locus.transform.position);
        }
        else
        {
            attackBasic(locus);
        }

    }
    public void moveTowardsNearestPlayer()
    {

    }
    public void attackBasic(GameObject target)//assumes already in range
    {

    }

    public void move(Vector2 targetPos)
    {
        Vector2 direction = targetPos - (Vector2)transform.position;
        direction.Normalize();
        float maxSpeedLimiter = (entity.maxSpeed-GetComponent<Rigidbody2D>().velocity.magnitude)/entity.maxSpeed;
        if (maxSpeedLimiter > 0 && entity.acceleration >0)
            GetComponent<Rigidbody2D>().AddForce(direction * entity.acceleration *1000 * Time.deltaTime * maxSpeedLimiter);
    }
}
