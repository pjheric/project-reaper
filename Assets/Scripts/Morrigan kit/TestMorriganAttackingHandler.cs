using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class TestMorriganAttackingHandler : MonoBehaviour
{
    [SerializeField] Mkit morriganKit;
    [SerializeField] playerSFX sfx;

    [SerializeField] GameObject sword;
    [SerializeField] Transform directionObj;

    [SerializeField] GameObject attackAreaObj;


    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

    [SerializeField] Animator animator;

    private bool canSwing = true;

    //[SerializeField] private float weaponDistance = -1.7f;
    [SerializeField] private float swingDegrees;
    [SerializeField] private float swingTime;
    private float orbitDegreesPerSec;

    [SerializeField]
    private float swingDelayTime;

    float swingTimer;
    float delayTimer = 0;

    private void Awake()
    {
        sword.SetActive(true);
        canSwing = true;
    }

    public void OnFire(InputValue value)
    {
        if (lockPlayerManager.morriganLock == false)
        {
            BasicAttack();
        }
    }

    void BasicAttack()
    {
        Vector3 audioPos = Vector3.right*2;      
        GameObject temp = Instantiate(sfx.audioPrefab,audioPos,Quaternion.identity);//spawns in left ear
        temp.GetComponent<SFXRunner>().clip = sfx.attack;
        if (canSwing)
        {
            animator.SetTrigger("attack");


            canSwing = false;

            Collider2D[] allHitColliders = Physics2D.OverlapAreaAll(point1.position, point2.position);

            foreach (var x in allHitColliders)
            {
                if (x.transform.tag == "enemy")
                {
                    var victimEntity = x.GetComponent<Entity>();
                    victimEntity.currentHealth -= morriganKit.BasicAttackDamage;
                    victimEntity.knockBack(morriganKit.BasicAttackKnockback, transform.gameObject);
                    victimEntity.hurtColor();

                }
            }
        }
        else
        {
        }
    }

    private void Update()
    {
        morriganKit.TrackPassive();
        ManageWeapon();
        delayTimer += Time.deltaTime * (morriganKit.GetPassiveBonusAttackSpeed/100 + 1);

        if (delayTimer >= swingDelayTime)
        {
            delayTimer = 0;
            canSwing = true;
        }
    }

    private void ManageWeapon()
    {
        // rotation
        float angle = directionObj.localRotation.eulerAngles.z - sword.transform.localRotation.eulerAngles.z;
        sword.transform.RotateAround(transform.position, Vector3.forward, angle);
        //clonedSword.GetComponent<Transform>().eulerAngles = newRotation;

        // position
        //clonedSword.transform.position = transform.position + transform.TransformDirection(new Vector3(-1.7f, 0, 0));
    }
}
