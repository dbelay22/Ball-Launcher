using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Utilities;
using Yxp.Debug;

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
        }

        void CheckSerializedFields()
        {
            _serializedFieldsError = false;

            if (_clonesParent == null)
            {
                YLogger.Error("BallHandler] _clonesParent is invalid");
                _serializedFieldsError = true;
            }

            if (_ballPrefab == null)
            {
                YLogger.Error("BallHandler] _ballPrefab is invalid");
                _serializedFieldsError = true;
            }

            if (_pivot == null)
            {
                YLogger.Error("BallHandler] _pivot is invalid");
                _serializedFieldsError = true;
            }
        }

        bool AnyError()
        {
            if (_serializedFieldsError)
            {
                YLogger.Error($"Update) AnyError is TRUE");
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

            SpawnBall();
        }

        void Update()
        {
            if (AnyError()) return;

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
            //YLogger.Debug("OnTouchPressing)...");

            if (_canDragBall)
            {
                _isDragging = true;

                DragBall();
            }

        }

        void OnTouchRelease()
        {
            //YLogger.Debug("OnTouchRelease)... ");

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
            YLogger.Debug("SpawnBall) ...");

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

            //YLogger.Debug($"[BallHandler] DragBall) touchWorldPos: {touchWorldPos}");

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

            YLogger.Debug($"Will invoke SpawnBall in {_respawnDelay} seconds");

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

            ReadOnlyArray<Touch> activeTouches = Touch.activeTouches;
            
            int activeTouchesCount = activeTouches.Count;

            if (activeTouchesCount > 1)
            {
                YLogger.Verbose($"Touch activeTouches now is {Touch.activeTouches.Count}");
            }

            int validTouches = 0;

            foreach (Touch touch in activeTouches)
            {                
                //YLogger.Debug($"GetWorldTouchPosition) Touch[{touch.touchId}, valid:{touch.valid}] screenPosition:{touch.screenPosition} /touch instance/");

                bool isTouchValid = !touch.screenPosition.x.Equals(float.PositiveInfinity) &&
                                    !touch.screenPosition.y.Equals(float.PositiveInfinity);

                if (isTouchValid)
                {
                    touchScreenPos += touch.screenPosition;

                    validTouches++;
                }                
            }

            if (validTouches < 1)
            {
#if UNITY_EDITOR
                YLogger.Verbose("GetWorldTouchPosition) No valid touches!");
#else
                YLogger.Warning("GetWorldTouchPosition) No valid touches!");
#endif

                return Vector3.one;
            }

            touchScreenPos /= validTouches;

            //YLogger.Debug($"GetWorldTouchPosition) touchScreenPos:{touchScreenPos}");

            // convert screen -> world position
            Vector3 touchWorldPos = _mainCamera.ScreenToWorldPoint(touchScreenPos);

            //YLogger.Debug($"GetWorldTouchPosition) touchWorldPos:{touchWorldPos} /WORLD/");

            return touchWorldPos;
        }

    }

}