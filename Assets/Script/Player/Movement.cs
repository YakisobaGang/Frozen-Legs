using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YakisobaGang.Script.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;
        private PlayerInputActions _input;
        private Transform _myTransform;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _input = new PlayerInputActions();
            _myTransform = GetComponent<Transform>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _input.Enable();
            _input.Gameplay.SelectMoveDirection.performed += SelectMoveDirection;
            _input.Gameplay.Move.performed += MoveDirection;
        }

        private void OnDisable()
        {
            _input.Disable();
            _input.Gameplay.SelectMoveDirection.performed -= SelectMoveDirection;
            _input.Gameplay.Move.performed -= MoveDirection;
        }

        private void MoveDirection(InputAction.CallbackContext ctx)
        {
            _rigidbody.AddForce(_myTransform.forward * speed, ForceMode.Impulse);
        }

        private void SelectMoveDirection(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            
            if(direction == Vector2.up)
                _myTransform.rotation = Quaternion.identity;

            if (direction == Vector2.down)
                _myTransform.rotation = Quaternion.Euler(0, 180, 0);

            if (direction == Vector2.left)
                _myTransform.rotation = Quaternion.Euler(0, -90, 0);

            if (direction == Vector2.right)
                _myTransform.rotation = Quaternion.Euler(0, 90, 0);
        }

    }
}