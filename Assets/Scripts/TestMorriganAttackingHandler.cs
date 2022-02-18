using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMorriganAttackingHandler : MonoBehaviour
{

    [SerializeField] Mkit morriganKit;

    [SerializeField] GameObject sword;
    [SerializeField] Transform directionObj;

    [SerializeField] Transform point1;
    [SerializeField] Transform point2;

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
        BasicAttack();
    }

    void BasicAttack()
    {
        Debug.Log("A*WDHHAOLD");

        if (canSwing)
        {
            Debug.Log("attack!");

            canSwing = false;

            Collider2D[] allHitColliders = Physics2D.OverlapAreaAll(point1.position, point2.position);

            foreach (var x in allHitColliders)
            {
                if (x.transform.tag == "enemy")
                {
                    var victimEntity = x.GetComponent<Entity>();
                    victimEntity.currentHealth -= morriganKit.BasicAttackDamage(victimEntity);
                    Debug.Log("enemy hit! health: " + victimEntity.currentHealth);
                    victimEntity.knockBack(morriganKit.BasicAttackKnockback(), transform.gameObject);
                    victimEntity.hurtColor();

                }
            }
        }
        else
        {
            Debug.Log("Cannot attack!");
        }
    }

    private void Update()
    {
        ManageWeapon();
        delayTimer += Time.deltaTime;

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
