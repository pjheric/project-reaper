using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


// public class Enemy : MonoBehaviour
// {
//     static int enemyCount;

    

//     float attackRange;
//     float attackDamage;
//     GameObject me;
//     public Enemy(GameObject Me, float MaxHealth)
//     {
//         maxHealth = MaxHealth;
//         currentHealth = maxHealth;
//         me = Me;
//     }
// //the base class
// //will have health, basic movement, etc
// //lots of behaviour like dashing and auras and attacking
// //the subclasses will each have specific functions that bring together components from the base class into the main functions that will abe called by enemy like (move, attack, special, etc)
//     protected void moveTowardsLocus(GameObject locus)
//     {
//         if (Vector2.Distance(locus.transform.position, me.transform.position)> attackRange + locus.GetComponent<CircleCollider2D>().radius)
//         {
//             move(locus.transform.position);
//         }
//         else
//         {
//             attackBasic(locus);
//         }

//     }
//     protected void moveTowardsNearestPlayer()
//     {

//     }
//     protected void attackBasic(GameObject target)//assumes already in range
//     {

//     }

//     protected void move(Vector2 targetPos)
//     {
//         Vector2 direction = targetPos - (Vector2)me.transform.position;
//         float maxSpeedLimiter = (maxSpeed-me.GetComponent<Rigidbody2D>().velocity.magnitude)/maxSpeed;
//         if (maxSpeedLimiter < 0)
//             maxSpeedLimiter = 0;
//         me.GetComponent<Rigidbody2D>().AddForce(direction * acceleration *1000 * Time.deltaTime * maxSpeedLimiter);
//     }

//     public virtual void act(GameObject target)//called every update
//     {
//     }
//     public virtual void attack(GameObject target)
//     {

//     }
// }
// public class BasicEnemy : Enemy
// {
//     public BasicEnemy(GameObject Me, float MaxHealth) : base(Me, MaxHealth)
//     {

//     }
//     public override void act(GameObject locus)//called every update
//     {
//         moveTowardsLocus(locus);
//     }
//     public override void attack(GameObject target)
//     {
        
//     }

// }