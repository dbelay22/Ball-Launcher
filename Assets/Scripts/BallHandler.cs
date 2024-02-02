using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
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

        Vector2 touchScreenPos = touch.position.ReadValue();

        Vector3 touchWorldPos = _mainCamera.ScreenToWorldPoint(touchScreenPos);

        Debug.Log($"[BallHandler] touchWorldPos: {touchWorldPos}, touchScreenPos: {touchScreenPos}");
    }
}
