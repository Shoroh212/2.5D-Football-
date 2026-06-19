using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce = 5f;

    private InputManager input;

    [Inject]
    public void Construct(InputManager inputManager)
    {
        input = inputManager;
    }

    private void OnEnable()
    {
        input.JumpPressed += Jump;
    }

    private void OnDisable()
    {
        input.JumpPressed -= Jump;
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}