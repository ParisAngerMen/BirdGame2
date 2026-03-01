using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Tilemaps;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    public float speed;
    public float jumpHeight;
    public float acceleration;
    public float decceleration;
    public float gravPowrr;
    public float frictionAmount;
    public float jumpAmount;

    private float velPower = 1f;
    private float timeInAir;
    private bool isGliding;
    private float initialDrag;
    private float currentJump = 1f;

    [Header("Jump Cut")]                                       
    [Range(0f, 1f)]                                              
    [SerializeField] private float jumpCutMultiplier = 0.4f;     
    [SerializeField] private float fallGravityMultiplier = 1.5f; 
    private bool isJumpCut;

    [Header("Gliding")]
    [SerializeField] private float timeToDeployWings;
    [SerializeField] private float distanceToDeployWings;
    [SerializeField] private float dragValue;
    [SerializeField] private LayerMask groundMask;

    [Header("Inputs")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference glideAction;
    public InputActionReference attackAction;

    [Header("Booleans")]
    public bool groundedPlayer;
    public bool isJumpPressed;
    public bool isJumpReleased;                                  
    public bool isGlidePressed;

    [Header("Attack")]
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float playerDamage;
    [SerializeField] public float attackRate;
    private float nextAttackTime = 0f;


    private bool isAttack;
    private bool isRight = true;

    private Vector2 moveDirection;
    private Vector3 playerVelocity;
    private Vector3 playerLastPos;

    private PlayerHealth health;

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        glideAction.action.Enable();
        attackAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        glideAction.action.Disable();
        attackAction.action.Disable();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        health = GetComponent<PlayerHealth>();
    }
    void Start()
    {
        initialDrag = rb.linearDamping;
    }

    private void Update()
    {
        isJumpPressed = jumpAction.action.WasPressedThisFrame();
        isJumpReleased = jumpAction.action.WasReleasedThisFrame();  
        isGlidePressed = glideAction.action.IsPressed();
        isAttack = Mouse.current.leftButton.wasPressedThisFrame;

        if (isJumpPressed && (groundedPlayer || currentJump <= jumpAmount))
        {
            Debug.Log("JUMP");
            rb.linearVelocityY = 0f;
            rb.AddForceY(jumpHeight, ForceMode2D.Impulse);
            currentJump++;
            isJumpCut = false;
        }

        if (isJumpReleased && !groundedPlayer && rb.linearVelocityY > 0f)
        {
            isJumpCut = true;
            rb.linearVelocityY *= jumpCutMultiplier;

        }

        if (!groundedPlayer)
        {
            timeInAir += Time.deltaTime;
            CanGlide();

            if (rb.linearVelocityY < 0f || isJumpCut)                 
            {                                                          
                rb.AddForceY(gravPowrr * fallGravityMultiplier, ForceMode2D.Force); 
            }    
            
            else                                                       
            {                                                          
                rb.AddForceY(gravPowrr, ForceMode2D.Force);
            }
        }

        else
        {
            currentJump = 1;
            SavePosition(transform.position);
        }

        if (Time.time >= nextAttackTime)
        {
            if (isAttack)
            {
                Debug.Log("Attack Pressed");
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();

        if (moveDirection.x > 0f && !isRight)
        {
            FlipCharacter();
        }

        else if (moveDirection.x < 0f && isRight)
        {
            FlipCharacter();
        }

        float topSpeed = moveDirection.x * speed;

        float speedDif = topSpeed - rb.linearVelocityX;

        float accelRate = (Mathf.Abs(topSpeed) > 0.01f) ? acceleration : decceleration;

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForceX(movement, ForceMode2D.Force);

        if (groundedPlayer && Mathf.Abs(moveDirection.x) < 0.01f)
        {
            timeInAir = 0f;
            float amount = Mathf.Min(Mathf.Abs(rb.linearVelocityX), Mathf.Abs(frictionAmount));

            amount *= Mathf.Sign(rb.linearVelocityX);

            rb.AddForceX(-amount, ForceMode2D.Impulse);
        }

        if (CanGlide() && isGlidePressed)
        {
            Glide();
        }

        else if (!isGlidePressed || !CanGlide())
        {
            rb.linearDamping = 0f;
        }
    }

    private void Glide()
    {
        rb.linearDamping = dragValue;
        Debug.Log("isGliding");
    }

    private bool CanGlide()
    {
        if (timeInAir >= timeToDeployWings)
        {
            Debug.Log("can glide");
            return !Physics2D.Raycast(transform.position, Vector2.down, timeToDeployWings, groundMask);
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
    }

    private void DebugChecking()
    {

    }

    void FlipCharacter()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isRight = !isRight;
    }

    void SavePosition(Vector3 pos)
    {
        playerLastPos = pos;
        playerLastPos.y = playerLastPos.y + 10f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("COIN!");
            collision.gameObject.GetComponent<Coin>().AddCoin();
        }

        if (collision.gameObject.layer == 8)
        {
            health.Die();
            health.healthVisual.SetHealth(0);
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            health.TakeDamage(1);
            Debug.Log("Collided with spikes");
        }


        if (collision.gameObject.CompareTag("Health"))
        {
            health.Heal(2);
            collision.gameObject.SetActive(false);
        }
    }

    private void Attack()
    {


        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRadius, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            enemy.gameObject.GetComponent<EnemyHealth>().TakeDamage(playerDamage);
            Debug.Log("ouch");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
