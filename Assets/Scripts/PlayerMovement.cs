using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;
    public float jumpHeight; 

    public InputActionReference moveAction;
    public InputActionReference jumpAction;

    public bool groundedPlayer;

    private Vector2 moveDirection;
    private Vector3 playerVelocity;

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();
        rb.linearVelocityX = moveDirection.x * speed;

        if (jumpAction.action.WasPressedThisFrame() && groundedPlayer)
        {
            Debug.Log("Jump pressed");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision detected");
    }
}
