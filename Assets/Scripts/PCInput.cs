using System;
using UnityEngine;

public class PCInput : InputManager
{
    public event Action<Vector2> Horizontal;
    public event Action<Vector2> Vertical;
    public event Action<Vector2> Interactive;
}
