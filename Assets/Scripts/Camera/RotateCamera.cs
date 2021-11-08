using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using YakisobaGang.Script.Player;

namespace YakisobaGang.Camera
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField] private AnimationCurve accelerationCurve;
        [SerializeField] private float speed = 3f;
        [SerializeField] private Transform center;

        private PlayerInputActions _input;
        private InputAction _rotateAction;
        private Transform _myTransform;

        private void Awake()
        {
            _input = new PlayerInputActions();
            _rotateAction = _input.Gameplay.RoteteCamera;
            _myTransform = transform;
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void Update()
        {
            if (_rotateAction.ReadValue<float>() < 0)
            {
                _myTransform.RotateAround(center.position, Vector3.down, accelerationCurve.Evaluate(Time.smoothDeltaTime) * speed);
            }
            else if (_rotateAction.ReadValue<float>() > 0)
            {
                _myTransform.RotateAround(center.position, Vector3.up, accelerationCurve.Evaluate(Time.smoothDeltaTime) * speed);
            }
        }
    }
}
