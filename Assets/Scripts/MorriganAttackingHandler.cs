using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MorriganAttackingHandler : MonoBehaviour
{
    private AttackingControls attackControls;

    [SerializeField] GameObject spear;
    [SerializeField] Transform directionObj;

    public bool thrusting;
    private bool canThrust;

    //[SerializeField] private float weaponDistance = -1.7f;
    [SerializeField] private float thrustTime;
    [SerializeField] private float thrustDelayTime;
    [SerializeField] private float thrustDistance;

    float thrustTimer;
    float delayTimer = 0;

    private void Awake()
    {
        attackControls = new AttackingControls();
        attackControls.MorriganAttack.BasicAttack.performed += BasicAttackContext => BasicAttack();
        spear.SetActive(true);
        canThrust = true;
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
        if (canThrust)
        {
            Debug.Log("attack!");

            canThrust = false;
            thrusting = true;
        }
        else
        {
            Debug.Log("Cannot attack!");
        }
    }

    private void Update()
    {
        if (thrusting)
        {
            // var v = Quaternion.AngleAxis(Time.time * speed * -10, Vector3.up) * new Vector3(distance, 0, 0);
            // clonedSword.transform.position = transform.position + v;
            thrustTimer += Time.deltaTime;
            
            // must add physics to thrust spear forward
        }
        else
        {
            ManageWeapon();
            delayTimer += Time.deltaTime;
        }
        if (thrustTimer >= thrustTime)
        {
            thrustTimer = 0;
            thrusting = false;
            canThrust = false;
            delayTimer = 0;
        }
        if (delayTimer >= thrustDelayTime)
        {
            delayTimer = 0;
            canThrust = true;
        }
    }

    private void ManageWeapon()
    {
        // rotation
        float angle = directionObj.localRotation.eulerAngles.z - spear.transform.localRotation.eulerAngles.z;
        spear.transform.RotateAround(transform.position, Vector3.forward, angle);
        //clonedSword.GetComponent<Transform>().eulerAngles = newRotation;

        // position
        //clonedSword.transform.position = transform.position + transform.TransformDirection(new Vector3(-1.7f, 0, 0));
    }
}
