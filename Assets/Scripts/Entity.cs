using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public bool isFriendly;

    public float maxHealth;
    public float currentHealth;

    public float acceleration;//might be 0
    public float maxSpeed;//might be 0
    
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
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
    
}
