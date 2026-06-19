using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : MonoBehaviour
{
    private GameInput gameI;
    private Rigidbody rb;


  [SerializeField] private float moveSpeed = 5f;
    private Vector2 moveInput;


  
   private int jumpCount= 0;

    private void Awake()
    {
        gameI = new GameInput();
        gameI.Player.Move.performed += context => OnMove(context);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    [Inject]
    public void Construct(GameInput input)
    {
        gameI = input;
    }

    private void OnEnable()
    {
        gameI.Enable();

        gameI.Player.Move.performed += OnMove;
        gameI.Player.Move.canceled += OnMove;
        gameI.Player.Jump.performed += OnJump;


    }

    private void OnDisable()
    {

        gameI.Player.Move.performed -= OnMove;
        gameI.Player.Move.canceled -= OnMove;
        gameI.Player.Jump.performed -= OnJump;
        gameI.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
       
        moveInput = ctx.ReadValue<Vector2>();

    }

    private void OnJump(InputAction.CallbackContext ctx)
    {

        if ( jumpCount < 2)
        {
            rb.AddForce(Vector3.up * 4f, ForceMode.Impulse);
            jumpCount += 1;

        }
      
    
    }

    private void FixedUpdate()
    {
        if (moveInput.x == 0) return;

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * moveSpeed;
        rb.linearVelocity = velocity;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
          
            jumpCount = 0;
        }
    }

}


public interface IInputService
{
    GameInput Input { get; }
}