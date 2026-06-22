using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : MonoBehaviour
{
    private GameInput gameI;
    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;
    private int jumpCount = 0;

    private void Awake()
    {
        gameI = new GameInput();


    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
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

        
       gameI.Player.Kick.performed += OnKick;
    }

    private void OnDisable()
    {
        gameI.Player.Move.performed -= OnMove;
        gameI.Player.Move.canceled -= OnMove;

        gameI.Player.Jump.performed -= OnJump;

     
        gameI.Player.Kick.performed -= OnKick;

        gameI.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();

        bool isRunning = Mathf.Abs(moveInput.x) > 0.01f;
        animator.SetBool("IsRun", isRunning);

        // флип
        if (moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (jumpCount < 2)
        {
            rb.AddForce(Vector3.up * 4f, ForceMode.Impulse);
            jumpCount++;

            animator.SetBool("IsJump", true);
        }
    }

    private void OnKick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        { animator.SetTrigger("Kick");
            Collider[] hits = Physics.OverlapSphere(transform.position, 1.5f);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Ball"))
                {
                    Rigidbody ballRb = hit.GetComponent<Rigidbody>();
                    if (ballRb)
                    {
                        Vector3 kickDir = (hit.transform.position - transform.position).normalized;
                        kickDir.y = 0.3f;
                        ballRb.AddForce(kickDir * 9f, ForceMode.Impulse);
                    }
                    break;
                }
            }
        }
    }
    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveInput.x * moveSpeed;
        rb.linearVelocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            jumpCount = 0;

            animator.SetBool("IsJump", false);
        }
    }
}

public interface IInputService
{
    GameInput Input { get; }
}