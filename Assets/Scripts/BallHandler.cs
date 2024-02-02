using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        UnityEngine.InputSystem.Controls.TouchControl touch = Touchscreen.current.primaryTouch;

        if (!touch.press.isPressed)
        {
            return;
        }

        Vector2 touchPos = touch.position.ReadValue();

        Debug.Log($"[BallHandler] touchPos: {touchPos}");
    }
}
