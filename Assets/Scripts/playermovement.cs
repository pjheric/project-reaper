using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playermovement : MonoBehaviour
{
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed;
    
    private PlayerInput playerInput;
    private InputAction moveAction;



    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
    }


    void Start()
    {
        
    }

    void Update()
    {
        moveManager();
    }

    void moveManager()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 move = moveAction.ReadValue<Vector2>();
        if (move.SqrMagnitude() != 0)
        {
            float maxSpeedLimiter = (maxSpeed-rb.velocity.magnitude)/maxSpeed;
            if (maxSpeedLimiter < 0)
                maxSpeedLimiter = 0;
            rb.AddForce(move * acceleration *1000 * Time.deltaTime * maxSpeedLimiter);
        }
    }
}
//     void FixedUpdate()
//     {
        
//     }
//     void onMove(InputValue value)
//     {
//         var move = value.Get<Vector2>();
//         var rb = gameObject.GetComponent<Rigidbody2D>();
//         print(rb.velocity.magnitude);
//     }
// }
