using UnityEngine;

namespace YakisobaGang.Camera
{
    public class FaceCamera : MonoBehaviour
    {
        private Transform _mainCameraTransform;

        private void Awake()
        {
            _mainCameraTransform = UnityEngine.Camera.main.transform;
        }

        private void LateUpdate()
        {
            var rotation = _mainCameraTransform.rotation;
            transform.LookAt(
                transform.position + rotation * Vector3.forward,
                rotation * Vector3.up);
        }
    }
}