using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playermovement : MonoBehaviour
{
    private ProjectReaper playerControls;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float maxSpeed;
    

    private void Awake()
    {
        playerControls = new ProjectReaper();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
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
        Vector2 move = playerControls.Player.Move.ReadValue<Vector2>();
        float maxSpeedLimiter = (maxSpeed-rb.velocity.magnitude)/maxSpeed;
        if (maxSpeedLimiter < 0)
            maxSpeedLimiter = 0;
        rb.AddForce(move * acceleration *1000 * Time.deltaTime * maxSpeedLimiter);
    }
    void FixedUpdate()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        print(rb.velocity.magnitude);
    }
    // void onMove(InputValue value)
    // {
    //     var moveInput = value.Get<Vector2>();
    //     print("wowo");
    // }
}
