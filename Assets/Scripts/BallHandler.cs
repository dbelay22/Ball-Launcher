using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject _clonesParent;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Rigidbody2D _pivot;    
    [SerializeField] private float _detachDelay;    
    [SerializeField] private float _respawnDelay;

    bool _serializedFieldsError = false;

    Camera _mainCamera;

    TouchControl _touch;

    bool _isDragging;

    Rigidbody2D _currentBallRB;

    SpringJoint2D _currentBallSpringJoint;

    GameObject _ballInstance;

    void Awake()
    {
        CheckSerializedFields();
    }

    void CheckSerializedFields() 
    {
        _serializedFieldsError = false;

        if (_clonesParent == null)
        {
            Debug.LogError("BallHandler] _clonesParent is invalid");
            _serializedFieldsError = true;
        }

        if (_ballPrefab == null)
        {
            Debug.LogError("BallHandler] _ballPrefab is invalid");
            _serializedFieldsError = true;
        }

        if (_pivot == null)
        {
            Debug.LogError("BallHandler] _pivot is invalid");
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

        _ballInstance = null;

        _mainCamera = Camera.main;

        _touch = Touchscreen.current.primaryTouch;

        SpawnBall();
    }

    void Update()
    {
        if (AnyError() == true) { return; }

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

    void SpawnBall()
    {
        if (_ballInstance != null)
        {
            // destroy previous ball instance
            Destroy(_ballInstance, 1f);
        }

        // new ball !!
        _ballInstance = Instantiate(_ballPrefab, _pivot.position, Quaternion.identity);

        // make it a child of "clones" empty object
        _ballInstance.transform.parent = _clonesParent.transform;

        // get components
        _currentBallRB = _ballInstance.GetComponent<Rigidbody2D>();

        _currentBallSpringJoint = _ballInstance.GetComponent<SpringJoint2D>();

        // enable spring joint
        EnableSpringJoint(true);
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

        Invoke(nameof(DisableSpringJoint), 0.5f);

        Invoke(nameof(SpawnBall), 3f);
    }

    void DisableSpringJoint()
    {
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
