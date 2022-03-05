using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class Player : MonoBehaviour
{
    [SerializeField] Entity entity;
    public Slider healthBar;
    [SerializeField] float outOfBoundsTickCooldown;
    [SerializeField] float outOfBoundsDamage;
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
            healthBar.value = entity.currentHealth;
        }
        
        if(Vector2.Distance(Vector2.zero, transform.position) > waveManager.publicMapRadius +5)
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
