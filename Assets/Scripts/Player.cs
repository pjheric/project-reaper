using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 

public enum Character
{
    morrigan,
    ganglim
}
public class Player : MonoBehaviour
{
    [SerializeField] Entity entity;
    public Character playerChar;
    public Slider healthBar;
    public TextMeshProUGUI healthText; 
    [SerializeField] float outOfBoundsTickCooldown;
    [SerializeField] float outOfBoundsDamage;
    [SerializeField] float deathStasisTime;
    float outOfBoundsTickCooldownTimer;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject); 
    }

    // Update is called once per frame
    void Update()
    {
        if (PersistentData.isGameStarted)
        {
            if(entity.currentHealth < 0){entity.currentHealth = 0;}
            healthBar.value = entity.currentHealth;
            healthText.text = entity.currentHealth.ToString() + "/" + entity.maxHealth.ToString(); 
        }
        if(PersistentData.isGameStarted)
        {
            if(entity.currentHealth <= 0)
            {
                if(((lockPlayerManager.ganglimLock == false && playerChar == Character.ganglim) || (lockPlayerManager.morriganLock == false && playerChar == Character.morrigan)))
                {
                    StartCoroutine(deathStasis());
                }
            }
        }
        //out of bounds damage check and run
        outOfBoundsDamageCheckFunc();
    }
    IEnumerator deathStasis()
    {
        if(playerChar == Character.morrigan)
        {
            lockPlayerManager.morriganLock = true;
        }
        if(playerChar == Character.ganglim)
        {
            lockPlayerManager.ganglimLock = true;
        }
        entity.spriteObjSprite.color = Color.gray;
        GetComponent<Rigidbody2D>().simulated = false;
        entity.originalColor = Color.gray;
        float t = 0.0f;
        while (t < deathStasisTime)
        {
            t+= Time.deltaTime;
            yield return null;
        }
        entity.spriteObjSprite.color = Color.white;
        GetComponent<Rigidbody2D>().simulated = true;
        entity.originalColor = Color.white;

        entity.addHealth(75);
        if(playerChar == Character.morrigan)
        {
            lockPlayerManager.morriganLock = false;
        }
        if(playerChar == Character.ganglim)
        {
            lockPlayerManager.ganglimLock = false;
        }
    }
    void outOfBoundsDamageCheckFunc()
    {
        if(((lockPlayerManager.ganglimLock == false && playerChar == Character.ganglim) || (lockPlayerManager.morriganLock == false && playerChar == Character.morrigan)) && Vector2.Distance(Vector2.zero, transform.position) > waveManager.publicMapRadius +5)
        {
            outOfBoundsTickCooldownTimer += Time.deltaTime;
            if(outOfBoundsTickCooldownTimer > outOfBoundsTickCooldown)
            {
                entity.currentHealth -= outOfBoundsDamage;
                entity.hurtColor();
                outOfBoundsTickCooldownTimer = 0.0f;
            }
        }
        else
        {
            outOfBoundsTickCooldownTimer = 0.0f;
        }
    }
}
