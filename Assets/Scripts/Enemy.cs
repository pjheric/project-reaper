using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Entity entity;
    [SerializeField]
    SpriteRenderer sprite;
    [SerializeField]
    Color baseColor;
    

    static int enemyCount;

    public float attackRange;
    public float attackDamage;
    public float attackDelay;
    public float attackKnockBackDistance;

    public bool isAttacking;

    waveManager WM;
    public GameObject locus;//will need to be set when it spawns
    public GameObject player1;
    public GameObject player2;

    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    void OnDestroy()
    {
        if(gameObject.scene.isLoaded) //Was Deleted
        {
            waveManager.enemyCount -=1;
        }
    }
    public void moveTowardsLocus()
    {
        float attackStartDistance = attackRange + locus.GetComponent<CircleCollider2D>().radius;
        if (Vector2.Distance((Vector3)locus.GetComponent<CircleCollider2D>().offset+locus.transform.position, transform.position)> attackStartDistance)
        {
            if(isAttacking == false){
                move(locus.transform.position);
            }
        }
        else
        {  
            attackBasic(locus, attackStartDistance);  
        }

    }
    public void moveTowardsNearestPlayer()
    {

    }
    public void checkDeath()
    {
        if (entity.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void CheckIfPlayerAttackable()
    {
        float attackStartDistance =  attackRange + player1.GetComponent<CapsuleCollider2D>().size.y;
        if (Vector2.Distance((Vector3)player1.GetComponent<CapsuleCollider2D>().offset+player1.transform.position, transform.position) < attackStartDistance)
        {
            attackBasic(player1, attackStartDistance);
        }
        attackStartDistance =  attackRange + player2.GetComponent<CapsuleCollider2D>().size.y;
        if (Vector2.Distance((Vector3)player2.GetComponent<CapsuleCollider2D>().offset+player2.transform.position, transform.position) < attackStartDistance)
        {
            attackBasic(player2, attackStartDistance);
        }
    }
    
    public void attackBasic(GameObject target, float attackStartDistance)//assumes in range at start of attack
    {
        if(isAttacking == false)
        {
            isAttacking = true;
            StartCoroutine(attackDelayer(target, attackStartDistance));
        }
    }

    IEnumerator attackDelayer(GameObject target, float attackStartDistance)
    {
        float t = 0.0f;
        while(true)
        {
            t += Time.deltaTime;
            sprite.color = new Color(1,1-t/attackDelay,1-t/attackDelay,1);
            if (t > attackDelay)
            {
                sprite.color = Color.blue;
                if (Vector2.Distance(target.transform.position+(Vector3)target.GetComponent<Collider2D>().offset, transform.position)<= attackStartDistance)
                {
                    //ACTUAL ATTACK
                    target.GetComponent<Entity>().currentHealth -= attackDamage;
                    target.GetComponent<Entity>().knockBack(attackKnockBackDistance,this.gameObject);
                }
                isAttacking = false;
                yield return new WaitForSeconds(0.05f);// stay on the full red for a little bit
                sprite.color = baseColor;//return to normal
                break;
            }
            yield return new WaitForEndOfFrame();
        }


    }

    public void auraAffect()
    {

    }

    public void move(Vector2 targetPos)
    {
        Vector2 direction = targetPos - (Vector2)transform.position;
        direction.Normalize();
        float maxSpeedLimiter = (entity.maxSpeed-GetComponent<Rigidbody2D>().velocity.magnitude)/entity.maxSpeed;
        if (maxSpeedLimiter > 0 && entity.acceleration >0)
            GetComponent<Rigidbody2D>().AddForce(direction * entity.acceleration *1000 * Time.deltaTime * maxSpeedLimiter);
        if(GetComponent<Rigidbody2D>().velocity.x> 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}
