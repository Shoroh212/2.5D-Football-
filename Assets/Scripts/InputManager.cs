using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : MonoBehaviour
{
    private GameInput gameI;

    private void Awake()
    {
        gameI = new GameInput();
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
    }

    private void OnDisable()
    {
        gameI.Player.Move.performed -= OnMove;
        gameI.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        Debug.Log("Move Input: " + input);
    }
}


public interface IInputService
{
    GameInput Input { get; }
}