using UnityEngine;
using Yxp.Debug;

namespace BallLauncher.Utils
{
    public class DestroyWhenOOS : MonoBehaviour
    {
        private Camera _camera;

        private Vector2 _screenMin;

        private Vector2 _screenMax;

        private float _objectWidth;

        private float _objectHeight;

        void Start()
        {
            _camera = Camera.main;

            _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
            _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;

            _screenMin = _camera.ViewportToWorldPoint(Vector2.zero);
            _screenMax = _camera.ViewportToWorldPoint(Vector2.one);

            /*
            YLogger.Debug($"screenMin.x:{_screenMin.x}, screenMin.y={_screenMin.y}");
            YLogger.Debug($"screenMax.x:{_screenMax.x}, screenMax.y={_screenMax.y}");
            */
        }

        void LateUpdate()
        {
            if (isTransformOOS())
            {
                YLogger.Debug("Sprite is OOS !! destroying it now...");
                Destroy(gameObject);
            }
        }

        bool isTransformOOS()
        {
            Vector3 viewPos = transform.position;

            bool outOfScreen = viewPos.x >= _screenMax.x + (_objectWidth / 2) ||
                               viewPos.x <= _screenMin.x - (_objectWidth / 2) ||
                               viewPos.y >= _screenMax.y + (_objectHeight / 2) ||
                               viewPos.y <= _screenMin.y - (_objectHeight / 2);

            return outOfScreen;
        }
    }

}