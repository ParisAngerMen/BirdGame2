using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
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

    private float velPower = 1f;
    private float timeInAir;
    private bool isGliding;
    private float initialDrag;

    [Header("Gliding")]
    [SerializeField] private float timeToDeployWings;
    [SerializeField] private float distanceToDeployWings;
    [SerializeField] private float dragValue;
    [SerializeField] private LayerMask groundMask;

    [Header("Inputs")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    public InputActionReference glideAction;

    [Header("Booleans")]
    public bool groundedPlayer;
    public bool isJumpPressed;
    public bool isGlidePressed;

    private Vector2 moveDirection;
    private Vector3 playerVelocity;

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
        glideAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
        glideAction.action.Disable();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void Start()
    {
        initialDrag = rb.linearDamping;
    }

    private void Update()
    {
        isJumpPressed = jumpAction.action.WasPressedThisFrame();
        isGlidePressed = glideAction.action.WasPerformedThisFrame();

        if (isJumpPressed && groundedPlayer)
        {
            Debug.Log("JUMP");
            rb.AddForceY(jumpHeight, ForceMode2D.Impulse);
        }

        if (!groundedPlayer)
        {
            timeInAir += Time.deltaTime;
            rb.AddForceY(gravPowrr, ForceMode2D.Force);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();

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

        else
        {

        }
    }

    private void Glide()
    {
        Debug.Log("isGliding");
    }

    private bool CanGlide()
    {
        if (timeInAir >= timeToDeployWings)
        {
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
}
