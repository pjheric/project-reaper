using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mspecial : Mpassive
{
    private bool doomBuffActive = false;
    private bool specialOnCooldown = false;
    private bool canSpecialStab = true;

    private float doomBuffTimer = 0;
    private float specialCooldownTimer = 20;

    private bool specialDidDamage = false;

    // if special is off cooldown, use special
    // if doom buff happens, start doom buff timer
    // when doom buff timer ends, start special cooldown

    public void OnSpecial(InputValue value)
    {
        if (lockPlayerManager.morriganLock == false)
        {
            StartSpecial();
        }
    }

    private void Update()
    {
        if (specialDidDamage)
        {
            UpdateSpecial();
        }

        if (doomBuffActive)
        {
            if (doomBuffTimer >= GetDoomBuffTime)
            {
                EndSpecial();
            }

            doomBuffTimer += Time.deltaTime;
        }

        if (specialOnCooldown)
        {
            if (specialCooldownTimer <= 0)
            {
                specialOnCooldown = false;
                canSpecialStab = true;
            }

            specialCooldownTimer -= Time.deltaTime;
        }
    }

    public void StartSpecial()
    {
        specialCooldownTimer = GetSpecialCooldown;

        // do attack stabby kachow
        if (!specialOnCooldown && canSpecialStab)
        {
            Debug.Log("start special");

            Collider2D[] allHitColliders = Physics2D.OverlapAreaAll(GetSpecialBorderPt1.position, GetSpecialBorderPt2.position);

            foreach (var x in allHitColliders)
            {
                if (x.transform.tag == "enemy")
                {
                    var victimEntity = x.GetComponent<Entity>();
                    victimEntity.currentHealth -= GetSpecialDamage;
                    Debug.Log("enemy hit! health: " + victimEntity.currentHealth);
                    victimEntity.hurtColor();
                    specialDidDamage = true;
                }
            }
        }

        canSpecialStab = false;
    }

    private void UpdateSpecial()
    {
        specialDidDamage = false;
        doomBuffActive = true;
        ActivateDoomEffect();
    }

    private void EndSpecial()
    {
        DeactivateDoomEffect();
        doomBuffActive = false;
        specialOnCooldown = true;
    }
}
