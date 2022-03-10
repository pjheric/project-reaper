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
    public float specialCooldownTimer = 0;

    private bool specialDidDamage = false;

    [SerializeField] playerSFX sfx;
    [SerializeField] GameObject specialAreaObj;
    [SerializeField] GameObject attackAreaObj;

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
        // do attack stabby kachow
        if (!specialOnCooldown && canSpecialStab)
        {

            //audio
            Vector3 audioPos = Vector3.right*1;      
            GameObject temp = Instantiate(sfx.audioPrefab,audioPos,Quaternion.identity);//spawns in left ear
            temp.GetComponent<SFXRunner>().clip = sfx.special;

            specialCooldownTimer = GetSpecialCooldown;
            specialAreaObj.SetActive(true);
            attackAreaObj.SetActive(false);
            Invoke("removeSpecialAreaIndicatorDelay",0.2f);

            Collider2D[] allHitColliders = Physics2D.OverlapAreaAll(GetSpecialBorderPt1.position, GetSpecialBorderPt2.position);

            foreach (var x in allHitColliders)
            {

                if (x.transform.tag == "enemy")
                {
                    var victimEntity = x.GetComponent<Entity>();
                    victimEntity.currentHealth -= GetSpecialDamage;
                    victimEntity.hurtColor();
                    specialDidDamage = true;
                }
            }
        }
        specialOnCooldown = true;
        canSpecialStab = false;
    }
    private void removeSpecialAreaIndicatorDelay()
    {
        specialAreaObj.SetActive(false);
        attackAreaObj.SetActive(true);
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
        //specialOnCooldown = true;
    }
}
