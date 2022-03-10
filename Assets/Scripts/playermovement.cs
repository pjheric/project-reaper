using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class playermovement : MonoBehaviour
{
    [SerializeField]
    Entity entity;
    [SerializeField]
    AnimationCurve JoystickMovementRampGraph;

    [SerializeField]
    float dashWaitTime;
    [SerializeField]
    float dashTime;
    [SerializeField]
    float dashSpeed;

    [SerializeField] Rigidbody2D rb;//just exists so I do less work :)
    [SerializeField] Transform directionObj;
    [SerializeField] GameObject spriteObj;
    [SerializeField] Animator animator;

    private float dashWaitTimeTimer = 10.0f;
    private float dashTimeTimer = 0.0f;

    private PlayerInput playerInput;
    //private InputAction moveAction;
    Vector2 rawMove;
    private InputAction dashAction;

    public bool isDashing;

    public bool spriteDirCorrect;

    //private IDualMotorRumble Gamepad;


    private void Awake()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
        lockPlayerManager.ganglimLock = true;
        lockPlayerManager.morriganLock = true;
        //moveAction = playerInput.actions["Move"];
        dashAction = playerInput.actions["Dash"];
    }


    void Start()
    {
        
    }

    void Update()
    {
        if ((gameObject.transform.GetComponent<GangLimAttackingHandler>() != null && lockPlayerManager.ganglimLock ==false) || (gameObject.transform.GetComponent<TestMorriganAttackingHandler>() != null && lockPlayerManager.morriganLock ==false))
        {
            moveManager();
        }
    }


    void moveManager()
    {
        //Movement
        if(animator != null){
            animator.SetFloat("speed", Mathf.Abs(rawMove.x) + Mathf.Abs(rawMove.y)/2);
        }
        if (rawMove.magnitude != 0){
            Vector2 move = rawMove*JoystickMovementRampGraph.Evaluate(rawMove.magnitude);
            if(animator == null || animator.GetCurrentAnimatorStateInfo(0).IsName("basicAttack") == false){
                if(move.x< 0)
                {
                    spriteObj.GetComponent<SpriteRenderer>().flipX = spriteDirCorrect;
                }
                else if (move.x > 0)
                {
                    spriteObj.GetComponent<SpriteRenderer>().flipX = !spriteDirCorrect;
                }
            }
            float maxSpeedLimiter = (entity.maxSpeed-rb.velocity.magnitude)/entity.maxSpeed;
            if (maxSpeedLimiter < 0)
                maxSpeedLimiter = 0;
            rb.AddForce(move * entity.acceleration *1000 * Time.deltaTime * maxSpeedLimiter);
            directionObj.up = GetComponent<Rigidbody2D>().velocity;
        }
        dashWaitTimeTimer += Time.deltaTime;     
        //Dash
        dashAction.started += context => dash();
        if(isDashing)
        {
            dashing();
        }
        // Debug.Log(dashWaitTimeTimer);
        
    }
    public void OnMove(InputValue value)
    {
        rawMove = value.Get<Vector2>();
    }


    void dash()
    {
        if ((gameObject.transform.GetComponent<GangLimAttackingHandler>() != null && lockPlayerManager.ganglimLock ==false) || (gameObject.transform.GetComponent<TestMorriganAttackingHandler>() != null && lockPlayerManager.morriganLock ==false))
        {
            if (dashWaitTimeTimer > dashWaitTime && isDashing == false)
            {
                Gamepad gamepad = InputSystem.GetDevice<Gamepad>();
                if (gamepad != null)
                    gamepad.SetMotorSpeeds(0.923f, 0.934f);
                isDashing = true;
                print("dash started");
            }
        }
    }

    //handles the high constant speed setting and handles stopping it and returning control after the time
    void dashing()
    {
        dashTimeTimer += Time.deltaTime;
        if (dashTimeTimer <= dashTime)
        {
            rb.velocity = directionObj.up * dashSpeed;
        }
        else
        {
            //done
            isDashing = false;
            
            print("dash finished");
            Gamepad gamepad = InputSystem.GetDevice<Gamepad>();
            if (gamepad != null)
                gamepad.SetMotorSpeeds(0f, 0f);
            dashWaitTimeTimer = 0.0f;
            dashTimeTimer = 0.0f;
        }
    }
    //TODO: add customizable rumble fade using asnc await

}
//     void FixedUpdate()
//     {
        
//     }
//     void onMove(InputValue value)
//     {
//         var move = value.Get<Vector2>();
//         print(rb.velocity.magnitude);
//     }
// }
