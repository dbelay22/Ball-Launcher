using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace BallLauncher.Components
{
    public class BallHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _clonesParent;
        [SerializeField] private GameObject _ballPrefab;
        [SerializeField] private Rigidbody2D _pivot;
        [SerializeField] private float _detachDelay;
        [SerializeField] private float _respawnDelay;

        bool _serializedFieldsError = false;

        Camera _mainCamera;

        bool _isDragging;

        Rigidbody2D _currentBallRB;

        SpringJoint2D _currentBallSpringJoint;

        GameObject _ballInstance;

        bool _canDragBall;

        void Awake()
        {
            CheckSerializedFields();

            if (AnyError())
            {
                return;
            }

            Application.targetFrameRate = 30;
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
            if (_serializedFieldsError == true)
            {
                Debug.LogError($"Update) AnyError is TRUE");
            }

            return _serializedFieldsError;
        }

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        void Start()
        {
            if (AnyError()) { return; }

            _isDragging = false;

            _canDragBall = false;

            _ballInstance = null;

            _mainCamera = Camera.main;

            Debug.Log("Start) Spawnining ball...");

            SpawnBall();
        }

        void Update()
        {
            if (AnyError() == true)
            {
                return;
            }

            ProcessInput();
        }

        void ProcessInput()
        {
            bool touchIsPressing = TouchIsPressed();

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
            //Debug.Log("OnTouchPressing)...");

            if (_canDragBall)
            {
                _isDragging = true;

                DragBall();
            }

        }

        void OnTouchRelease()
        {
            //Debug.Log("OnTouchRelease)... ");

            // was dragging ?
            if (_isDragging)
            {
                _isDragging = false;

                _canDragBall = false;

                LaunchBall();
            }
        }

        void SpawnBall()
        {
            Debug.Log("SpawnBall) ...");

            // new ball !!
            _ballInstance = Instantiate(_ballPrefab, _pivot.position, Quaternion.identity);

            // make it a child of "clones" empty object
            _ballInstance.transform.parent = _clonesParent.transform;

            // get components
            _currentBallRB = _ballInstance.GetComponent<Rigidbody2D>();

            _currentBallSpringJoint = _ballInstance.GetComponent<SpringJoint2D>();

            // connect ball to pivot
            _currentBallSpringJoint.connectedBody = _pivot;

            // enable spring joint
            EnableSpringJoint(true);

            _canDragBall = true;
        }

        void DragBall()
        {
            if (!_canDragBall)
            {
                return;
            }

            // disable physics for the ball's RB
            SetKinematic(true);

            Vector3 touchWorldPos = GetWorldTouchPosition();

            //Debug.Log($"[BallHandler] DragBall) touchWorldPos: {touchWorldPos}");

            // update ball's rigid body position
            _currentBallRB.position = touchWorldPos;
        }

        void LaunchBall()
        {
            // re-enable physics for the ball's RB
            SetKinematic(false);

            Invoke(nameof(DetachBall), _detachDelay);
        }

        void DetachBall()
        {
            EnableSpringJoint(false);

            Debug.Log($"Will invoke SpawnBall in {_respawnDelay} seconds");

            Invoke(nameof(SpawnBall), _respawnDelay);
        }

        void SetKinematic(bool kinematic)
        {
            _currentBallRB.isKinematic = kinematic;
        }

        void EnableSpringJoint(bool enable)
        {
            _currentBallSpringJoint.enabled = enable;
        }

        bool TouchIsPressed()
        {
            return Touch.activeTouches.Count > 0;
        }

        Vector3 GetWorldTouchPosition()
        {
            // read touch position
            var touchScreenPos = new Vector2();

            if (Touch.activeTouches.Count > 1)
            {
                Debug.Log($"Touch activeTouches now is {Touch.activeTouches.Count}");
            }

            foreach (Touch touch in Touch.activeTouches)
            {
                touchScreenPos += touch.screenPosition;
            }

            //Debug.Log($"GetWorldTouchPosition) touchScreenPos:{touchScreenPos} /BEFORE/");

            touchScreenPos /= Touch.activeTouches.Count;

            //Debug.Log($"GetWorldTouchPosition) touchScreenPos:{touchScreenPos} /AFTER/");

            if (touchScreenPos.x.Equals(float.PositiveInfinity) ||
                touchScreenPos.y.Equals(float.PositiveInfinity))
            {
                return Vector3.one;
            }

            // convert screen -> world position
            Vector3 touchWorldPos = _mainCamera.ScreenToWorldPoint(touchScreenPos);

            //Debug.Log($"GetWorldTouchPosition) touchWorldPos:{touchWorldPos} /WORLD/");

            return touchWorldPos;
        }

    }

}