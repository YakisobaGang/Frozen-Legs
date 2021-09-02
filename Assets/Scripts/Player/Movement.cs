using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YakisobaGang.Player
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement Settings"), Space]
        [SerializeField] private float speed = 100f;
        [SerializeField] private bool disableDiagonalMovement = true;
        
        [Header("Animation Settings"), Space]
        [SerializeField] private float rotationSpeed = 0.4f;
        [SerializeField] private Ease easeCurve;
        
        private readonly LookDirections _currentDirections = new LookDirections();
        private PlayerInputActions _input;
        private Transform _myTransform;
        private Rigidbody _rigidbody;

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
            if(!disableDiagonalMovement)
                return;
            
            // Enquanto o player tiver uma velocidade ele nao podera mudar de direcao
            // assim ele nao irar se movimentar na diagonal
            if(_rigidbody.velocity == Vector3.zero)
                _input.Enable();
            else
                _input.Disable();
        }

        private void MoveDirection(InputAction.CallbackContext ctx)
        {
            // Adiciona um impulso na direcao que esta olhado
            _rigidbody.AddForce(_currentDirections.MoveDir * speed, ForceMode.Impulse);
        }

        private void SelectMoveDirection(InputAction.CallbackContext ctx)
        {
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
    }
    
    public class LookDirections
    {
        public Vector3 LookVector { get; private set; }
        public Vector3 MoveDir { get; private set; }

        public LookDirections() => LookForward();

        public void LookForward()
        {
            LookVector = Vector3.zero;
            MoveDir = Vector3.forward;
        }

        public void LookBackward()
        {
            LookVector = new Vector3(0, 180, 0);
            MoveDir = Vector3.back;
        }

        public void LookLeft()
        {
            LookVector = new Vector3(0, -90, 0);
            MoveDir = Vector3.left;
        }

        public void LookRight()
        {
            LookVector = new Vector3(0, 90, 0);
            MoveDir = Vector3.right;
        }
    }
}