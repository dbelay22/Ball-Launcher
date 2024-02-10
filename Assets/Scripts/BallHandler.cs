using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _currentBallRB;

    [SerializeField] private SpringJoint2D _currentBallSpringJoint;

    bool _serializedFieldsError = false;

    Camera _mainCamera;

    TouchControl _touch;

    bool _isDragging;

    bool _launchedBall;

    void Awake()
    {
        CheckSerializedFields();
    }

    void CheckSerializedFields() 
    {
        _serializedFieldsError = false;

        if (_currentBallRB == null)
        {
            Debug.LogError("BallHandler] _currentBallRB is invalid");
            _serializedFieldsError = true;
        }

        if (_currentBallSpringJoint == null)
        {
            Debug.LogError("BallHandler] _currentBallSpringJoint is invalid");
            _serializedFieldsError = true;
        }
    }

    bool AnyError()
    {
        return _serializedFieldsError;
    }

    void Start()
    {
        if (AnyError()) { return; }

        _isDragging = false;

        _launchedBall = false;

        _mainCamera = Camera.main;

        _touch = Touchscreen.current.primaryTouch;

        EnableSpringJoint(true);
    }

    void Update()
    {
        if (AnyError() == true) { return; }

        if (_launchedBall == true) { return; }

        ProcessInput();
    }

    void ProcessInput()
    {
        bool touchIsPressing = touchIsPressed();

        if (touchIsPressing)
        {
            OnTouchPressing();
        }
        else
        {
            OnTouchRelease();
        }
    }

    void OnTouchPressing()
    {
        _isDragging = true;

        DragBall();
    }

    void OnTouchRelease()
    {
        // finger out
        if (_isDragging)
        {
            _isDragging = false;

            LaunchBall();
        }
    }

    void DragBall()
    {
        // disable physics for the ball's RB
        SetKinematic(true);

        // read touch position
        Vector2 touchScreenPos = _touch.position.ReadValue();

        if (touchScreenPos.x.Equals(float.PositiveInfinity) ||
            touchScreenPos.y.Equals(float.PositiveInfinity))
        {
            //Debug.Log("false touch positive INFINITY");
            return;
        }

        // convert screen -> world position
        Vector3 touchWorldPos = _mainCamera.ScreenToWorldPoint(touchScreenPos);

        //Debug.Log($"[BallHandler] touchWorldPos: {touchWorldPos}, touchScreenPos: {touchScreenPos}");

        // update ball's rigid body position
        _currentBallRB.position = touchWorldPos;
    }    

    void LaunchBall()
    {
        // re-enable physics for the ball's RB
        SetKinematic(false);

        StartCoroutine(DisableSpringJointDelayed());

        _launchedBall = true;
    }

    IEnumerator DisableSpringJointDelayed()
    {
        yield return new WaitForSeconds(1f);

        EnableSpringJoint(false);        
    }

    void SetKinematic(bool kinematic)
    {
        _currentBallRB.isKinematic = kinematic;
    }

    void EnableSpringJoint(bool enable)
    {
        _currentBallSpringJoint.enabled = enable;
    }

    bool touchIsPressed()
    {
        return _touch.press.isPressed;
    }
}
