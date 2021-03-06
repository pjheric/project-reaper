using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator anim;
    [SerializeField] enemySFX SFXObj;
    

    static int enemyCount;

    public float attackRange;
    public float attackDamage;
    public float attackTime;
    public float timeBetweenAttacks;
    float tBACounter = 10.0f;
    public float attackKnockBackDistance;

    public bool isAttacking;

    waveManager WM;
    public GameObject locus;//will need to be set when it spawns
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("attackSpeed", 0.153f/attackTime);
    }

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
    //will both move towards the target and also stop to attack when close enough
    public void moveAndAttack(GameObject target)
    {
        tBACounter += Time.deltaTime;
        float attackStartDistance = attackRange + target.GetComponent<CapsuleCollider2D>().size.y/1.9f;
        if (Vector2.Distance((Vector3)target.GetComponent<CapsuleCollider2D>().offset+target.transform.position, transform.position)> attackStartDistance)
        {
            if(isAttacking == false){
                move(target.transform.position);
            }
        }
        else
        {  
            attackBasic(target, attackStartDistance);  
        }

    }
    public void moveandAttackNearestPlayer()
    {
        //if the player is the closest and is not locked/stasis
        if(lockPlayerManager.ganglimLock == false && (Vector3.Distance(transform.position, player1.transform.position) < Vector3.Distance(transform.position, player2.transform.position) || lockPlayerManager.morriganLock == true))
        {
            moveAndAttack(player1);
        }
        else if(lockPlayerManager.morriganLock == false)
        {
            moveAndAttack(player2);
        }
        else
        {

        }
    }
    public void checkDeath()
    {
        if (entity.currentHealth <= 0)
        {
            Vector3 audioPos;
            if(Vector3.Distance(transform.position, player1.transform.position) < Vector3.Distance(transform.position, player2.transform.position))
            {
                audioPos = Vector3.left*2;
            }
            else
            {
                audioPos = Vector3.right*2;
            }
            GameObject temp = Instantiate(SFXObj.audioPrefab, audioPos, Quaternion.identity);//spawns in left ear
            temp.GetComponent<SFXRunner>().clip = SFXObj.death;
            Destroy(this.gameObject);
        }
    }

    public void CheckIfPlayerAttackable()
    {
        float attackStartDistance =  attackRange + player1.GetComponent<CapsuleCollider2D>().size.y/1.9f;
        if (Vector2.Distance((Vector3)player1.GetComponent<CapsuleCollider2D>().offset+player1.transform.position, transform.position) < attackStartDistance && lockPlayerManager.ganglimLock == false)
        {
            attackBasic(player1, attackStartDistance);
        }
        attackStartDistance =  attackRange + player2.GetComponent<CapsuleCollider2D>().size.y/1.9f;
        if (Vector2.Distance((Vector3)player2.GetComponent<CapsuleCollider2D>().offset+player2.transform.position, transform.position) < attackStartDistance && lockPlayerManager.morriganLock == false)
        {
            attackBasic(player2, attackStartDistance);
        }
    }
    
    public void attackBasic(GameObject target, float attackStartDistance)//assumes in range at start of attack
    {
        if(isAttacking == false && tBACounter > timeBetweenAttacks)
        {
            
            tBACounter = 0.0f;//redundant
            isAttacking = true;
            anim.SetTrigger("attack");
            StartCoroutine(attackDelayer(target, attackStartDistance));
        }
    }

    IEnumerator attackDelayer(GameObject target, float attackStartDistance)
    {
        float t = 0.0f;
        bool soundPlayed = false;
        while(true)
        {
            t += Time.deltaTime;
            //run the attack sound a little bit before the attack actually ends, but still calc distance, rather janky but also maybe good sounding
            if(t > attackTime - 0.2f && soundPlayed == false && Vector2.Distance(target.transform.position+(Vector3)target.GetComponent<Collider2D>().offset, transform.position)<= attackStartDistance)
            {
                soundPlayed = true;
                Vector3 audioPos;
                if(target.GetComponent<GLkit>() != null)
                {
                    audioPos = Vector3.left*2;
                }else if(target.GetComponent<Mkit>() != null)
                {
                    audioPos = Vector3.right*2;
                }
                else
                {
                    audioPos = Vector3.up*2;
                }
                GameObject temp = Instantiate(SFXObj.audioPrefab,audioPos,Quaternion.identity);//spawns in left ear
                temp.GetComponent<SFXRunner>().clip = SFXObj.Attack;
            }
            if (t > attackTime)
            {
                if (Vector2.Distance(target.transform.position+(Vector3)target.GetComponent<Collider2D>().offset, transform.position)<= attackStartDistance)
                {
                    //ACTUAL ATTACK
                    sprite.color = Color.blue;
                    target.GetComponent<Entity>().currentHealth -= attackDamage;
                    target.GetComponent<Entity>().hurtColor();
                    target.GetComponent<Entity>().knockBack(attackKnockBackDistance,this.gameObject);
                    
                    yield return new WaitForSeconds(0.05f);// stay on the full red for a little bit

                }
                isAttacking = false;
                tBACounter = 0.0f;
                sprite.color = entity.originalColor;//return to normal
                break;
            }
            yield return new WaitForEndOfFrame();
        }


    }

    public void auraAffect()
    {

    }

    //will move to the point
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

    //will continue infinetly in that direction rather than going to a specific point
    public void moveDir(Vector2 targetDir)
    {
        Vector2 direction = targetDir + (Vector2)transform.position;
        move(direction);
    }
}
