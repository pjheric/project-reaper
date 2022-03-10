using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GLspecial : GLpassive
{
    private bool specialActive = false;
    private bool specialOnCooldown = false;
    private float specialDurationTimer = 0;
    public float specialCooldownTimer = 0;
    [SerializeField] Animator anim;
    [SerializeField] playerSFX sfx;
    [SerializeField] GameObject specialAreaObj;
    [SerializeField] GameObject attackAreaObj;
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
        //run audio
        Vector3 audioPos = Vector3.left*1.5f;      
        GameObject temp = Instantiate(sfx.audioPrefab,audioPos,Quaternion.identity);//spawns in left ear
        temp.GetComponent<SFXRunner>().clip = sfx.special;
        temp.GetComponent<SFXRunner>().source.loop = true;
        temp.GetComponent<SFXRunner>().killInX(6);

        specialAreaObj.SetActive(true);
        attackAreaObj.SetActive(false);
        anim.SetBool("specialOn", true);
        specialDurationTimer = GetSpecialDuration;
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
        specialDurationTimer -= Time.deltaTime;
        if (specialDurationTimer <= 0)
        {
            EndSpecial();
        }

    }

    private void EndSpecial()
    {
        specialActive = false;
        specialAreaObj.SetActive(false);
        attackAreaObj.SetActive(true);
        anim.SetBool("specialOn", false);
        GetComponent<Collider2D>().enabled = true;
        CanBasicAttack = true;

        specialCooldownTimer = GetSpecialCooldown;
        specialOnCooldown = true;

    }
}
