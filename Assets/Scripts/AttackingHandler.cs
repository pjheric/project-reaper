using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackingHandler : MonoBehaviour
{
    private AttackingControls attackControls;

    [SerializeField] GameObject sword;
    [SerializeField] Transform directionObj;

    private bool swinging;
    private bool canSwing;

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
        attackControls = new AttackingControls();
        attackControls.PlayerAttack.BasicAttack1.performed += BasicAttackContext => BasicAttack();
        sword.SetActive(true);
        canSwing = true;
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
        if (canSwing)
        {
            Debug.Log("attack!");

            canSwing = false;
            swinging = true;

            float swingStartAngle = (swingDegrees / 2) * -1;

            //set sword angle back 90 degrees in prep for 18degree attack
            sword.transform.RotateAround(transform.position, Vector3.forward, swingStartAngle);
        }
        else
        {
            Debug.Log("Cannot attack!");
        }
    }

    private void Update()
    {
        orbitDegreesPerSec = swingDegrees / swingTime;

        if (swinging)
        {
            // var v = Quaternion.AngleAxis(Time.time * speed * -10, Vector3.up) * new Vector3(distance, 0, 0);
            // clonedSword.transform.position = transform.position + v;
            swingTimer += Time.deltaTime;
            sword.transform.RotateAround(transform.position, Vector3.forward, orbitDegreesPerSec * Time.deltaTime);
        }
        else
        {
            ManageWeapon();
            delayTimer += Time.deltaTime;
        }
        if(swingTimer >= swingTime)
        {
            swingTimer = 0;
            swinging = false;
            canSwing = false;
            delayTimer = 0;
        }
        if (delayTimer >= swingDelayTime)
        {
            delayTimer = 0;
            canSwing = true;
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
