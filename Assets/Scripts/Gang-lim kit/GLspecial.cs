using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GLspecial : GLpassive
{
    private bool specialActive = false;
    private bool specialOnCooldown = false;
    private float specialCooldownTimer = 8;

    public void OnSpecial(InputValue value)
    {
        if (specialOnCooldown == false && lockPlayerManager.ganglimLock == false)
        {
            StartSpecial();
        }
    }

    private void Update()
    {
        if (specialActive)
        {
            UpdateSpecial();
        }

        if (specialOnCooldown)
        {
            if (specialCooldownTimer <= 0)
            {
                specialOnCooldown = false;
            }

            specialCooldownTimer -= Time.deltaTime;
        }
    }

    public void StartSpecial()
    {
        specialActive = true;
        CanBasicAttack = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private void UpdateSpecial()
    {
        Collider2D[] allHitColliders = Physics2D.OverlapCircleAll(transform.position, GetSpecialRadius);

        foreach (var x in allHitColliders)
        {
            if (x.transform.tag == "enemy")
            {
                x.GetComponent<Entity>().currentHealth -= GetSpecialDPS * Time.deltaTime;
            }
        }

        if (specialCooldownTimer <= 0)
        {
            specialActive = false;
            EndSpecial();
        }

        specialCooldownTimer -= Time.deltaTime;
    }

    private void EndSpecial()
    {
        GetComponent<Collider2D>().enabled = true;
        specialCooldownTimer = GetSpecialCooldown;
        CanBasicAttack = true;
        specialOnCooldown = true;
    }
}
