using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GLspecial : GLpassive
{
    private bool specialActive = false;
    private bool specialOnCooldown = false;
    private float specialDurationTimer = 0;
    private float specialCooldownTimer = 0;
    [SerializeField] Animator anim;
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
        Debug.Log(specialCooldownTimer);
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
        Debug.Log(anim.GetBool("specialOn"));
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
