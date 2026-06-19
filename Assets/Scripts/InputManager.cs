using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : MonoBehaviour
{
    private GameInput gameI;

    [Inject]
    public void Construct(GameInput input)
    {
        gameI = input;
    }

    private void OnEnable()
    {
        gameI.Enable();

        gameI.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        gameI.Player.Jump.performed -= OnJump;
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        JumpPressed?.Invoke();
    }

    public event Action JumpPressed;
}