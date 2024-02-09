using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _currentBallRB;

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

        bool isPressed = touch.press.isPressed;

        SetKinematic(isPressed);

        if (!isPressed)
        {
            return;
        }       

        Vector2 touchScreenPos = touch.position.ReadValue();

        if (touchScreenPos.x.Equals(float.PositiveInfinity) ||
            touchScreenPos.y.Equals(float.PositiveInfinity))
        {
            //Debug.Log("false touch positive INFINITY");
            return;
        }

        Vector3 touchWorldPos = _mainCamera.ScreenToWorldPoint(touchScreenPos);

        //Debug.Log($"[BallHandler] touchWorldPos: {touchWorldPos}, touchScreenPos: {touchScreenPos}");

        _currentBallRB.position = touchWorldPos;
    }

    void SetKinematic(bool kinematic)
    {
        _currentBallRB.isKinematic = kinematic;
    }
}
