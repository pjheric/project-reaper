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

    }

    private void Update()
    {
        ManageWeapon();
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
