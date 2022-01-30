using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackingHandler : MonoBehaviour
{
    private AttackingControls attackControls;

    [SerializeField] private GameObject sword;

    private GameObject clonedSword;
    private Vector3 newRotation;

    private bool attacking;

    [SerializeField] private float weaponDistance = -1.7f;
    [SerializeField] private float orbitDegreesPerSec = 180.0f;

    private void Awake()
    {
        attackControls = new AttackingControls();
        attackControls.PlayerAttack.BasicAttack1.performed += BasicAttackContext => BasicAttack();

        clonedSword = Instantiate(sword);
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
    }

    private void Update()
    {
        if (attacking)
        {
            // var v = Quaternion.AngleAxis(Time.time * speed * -10, Vector3.up) * new Vector3(distance, 0, 0);
            // clonedSword.transform.position = transform.position + v;

            clonedSword.transform.position = transform.position + (transform.position - transform.position).normalized * weaponDistance;
            clonedSword.transform.RotateAround(transform.position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
        }
        if (!attacking)
        {
            ManageWeapon();
        }
    }

    private void ManageWeapon()
    {
        // rotation
        newRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.x, transform.eulerAngles.z + 90);
        clonedSword.GetComponent<Transform>().eulerAngles = newRotation;

        // position
        clonedSword.transform.position = transform.position + transform.TransformDirection(new Vector3(-1.7f, 0, 0));
    }
}
