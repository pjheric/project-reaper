using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Entity : MonoBehaviour
{
    public bool isFriendly;

    public float maxHealth;
    public float currentHealth;

    public float acceleration;//might be 0
    public float maxSpeed;//might be 0

    public SpriteRenderer spriteObjSprite;
    public Image edgeOfScreen; // can be null
    public Image edgeOfScreen2; // can be null
    public Color originalColor;
    
    // Start is called before the first frame update
    void Start()
    {
        originalColor = spriteObjSprite.color;
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    public void addHealth(int health)
    {
        currentHealth += health;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
    public void knockBack(float knockBackDistance, GameObject source)
    {
        Vector2 direction = transform.position - source.transform.position;
        direction.Normalize();
        GetComponent<Rigidbody2D>().AddForce(direction *2000 * knockBackDistance);
    }
    public void knockBack(float knockBackDistance, Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction *2000 * knockBackDistance);
    }
    public void hurtColor()//TEMP
    {
        spriteObjSprite.color = Color.red;
        if(edgeOfScreen != null)
        {
            edgeOfScreen.color = Color.red;
        }
        if(edgeOfScreen2 != null)
        {
            edgeOfScreen2.color = Color.red;
        }
        Invoke("resetColor",0.1f);
    }
    public void resetColor()
    {
        spriteObjSprite.color = originalColor;
        if(edgeOfScreen != null)
        {
            edgeOfScreen.color = originalColor;
        }
        if(edgeOfScreen2 != null)
        {
            edgeOfScreen2.color = originalColor;
        }
    }
    
}
