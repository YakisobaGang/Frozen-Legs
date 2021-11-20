using System;
using DG.Tweening;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.InputSystem;
using YakisobaGang.Script.Player;

namespace YakisobaGang.Player.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        [Header("Movement Settings"), Space]
        [SerializeField] private float speed = 100f;
        [SerializeField] private bool disableDiagonalMovement = true;
        [SerializeField, FMODUnity.EventRef] private string collsionSFX;
        [SerializeField, FMODUnity.EventRef] private string iceSlideSFX;

        [Header("Animation Settings"), Space]
        [SerializeField] private float rotationSpeed = 0.4f;
        [SerializeField] private Ease easeCurve;

        [Header("Movement VFX"), Space] 
        [SerializeField] private GameObject movementSmoke;
        [SerializeField] private GameObject movementSpell;
        
        private readonly LookDirections _currentDirections = new LookDirections();
        private PlayerInputActions _input;
        private Transform _myTransform;
        private Rigidbody _rigidbody;
        private bool canMove = true;
        private EventInstance iceSlideInstance;
        #region Steup

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

        #endregion

        private void Update()
        {
            if (!disableDiagonalMovement)
                return;

            // Enquanto o player tiver uma velocidade ele nao podera mudar de direcao
            // assim ele nao irar se movimentar na diagonal
            canMove = _rigidbody.velocity == Vector3.zero;
        }

        private void MoveDirection(InputAction.CallbackContext ctx)
        {
            if (!canMove)
                return;

            // Adiciona um impulso na direcao que esta olhado
            _rigidbody.AddForce(_currentDirections.MoveDir * speed, ForceMode.Impulse);
            
            movementSmoke.SetActive(true);
            iceSlideInstance = FMODUnity.RuntimeManager.CreateInstance(iceSlideSFX);
            iceSlideInstance.setParameterByName("StillInMovement", 1);
            iceSlideInstance.start();
        }

        private void SelectMoveDirection(InputAction.CallbackContext ctx)
        {
            if (!canMove)
                return;

            Vector2 direction = ctx.ReadValue<Vector2>();

            if (direction == Vector2.up)
                _currentDirections.LookForward();

            if (direction == Vector2.down)
                _currentDirections.LookBackward();

            if (direction == Vector2.left)
                _currentDirections.LookLeft();

            if (direction == Vector2.right)
                _currentDirections.LookRight();

            RotationTweener(Quaternion.Euler(_currentDirections.LookVector));
        }

        private void RotationTweener(Quaternion rotation)
        {
            _myTransform
                .DORotateQuaternion(rotation, rotationSpeed)
                .SetEase(easeCurve);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.CompareTag("Obstacles"))
            {
                FMODUnity.RuntimeManager.PlayOneShot(collsionSFX);
                GameFeelController.ApplyCameraShake();
                movementSmoke.SetActive(false);
                
                if (_rigidbody.velocity == Vector3.zero)
                {
                    iceSlideInstance.stop(STOP_MODE.ALLOWFADEOUT);
                    iceSlideInstance.setParameterByName("StillInMovement", 0);
                    iceSlideInstance.release();
                }
            }
        }
    }
}