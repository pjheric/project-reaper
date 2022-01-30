using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackingHandler : MonoBehaviour
{
    private AttackingControls attackControls;

    [SerializeField] GameObject sword;
    [SerializeField] Transform directionObj;

    private bool attacking;

    //[SerializeField] private float weaponDistance = -1.7f;
    [SerializeField] private float orbitDegreesPerSec;
    [SerializeField] private float swingTime;
    float swingTimer;

    private void Awake()
    {
        attackControls = new AttackingControls();
        attackControls.PlayerAttack.BasicAttack1.performed += BasicAttackContext => BasicAttack();
        sword.SetActive(true);
    }

    private void OnEnable()
    {
        attackControls.Enable();
    }

    private void OnDisable()
    {
        attackControls.Disable();
    }


    private void BasicAttack()
    {
        Debug.Log("attack!");

        attacking = true;
        //set sword angle back 90 degrees in prep for 18degree attack
        sword.transform.RotateAround(transform.position, Vector3.forward, -90);
    }

    private void Update()
    {
        if (attacking)
        {
            // var v = Quaternion.AngleAxis(Time.time * speed * -10, Vector3.up) * new Vector3(distance, 0, 0);
            // clonedSword.transform.position = transform.position + v;
            swingTimer += Time.deltaTime;
            sword.transform.RotateAround(transform.position, Vector3.forward, orbitDegreesPerSec * Time.deltaTime);
        }
        if (!attacking)
        {
            ManageWeapon();
        }
        if(swingTimer >= swingTime)
        {
            swingTimer = 0;
            attacking=false;
        }
    }

    private void ManageWeapon()
    {
        // rotation
        float angle = directionObj.localRotation.eulerAngles.z-sword.transform.localRotation.eulerAngles.z;
        sword.transform.RotateAround(transform.position, Vector3.forward, angle);
        //clonedSword.GetComponent<Transform>().eulerAngles = newRotation;

        // position
        //clonedSword.transform.position = transform.position + transform.TransformDirection(new Vector3(-1.7f, 0, 0));
    }
}
