using UnityEngine;
using UnityEngine.InputSystem;

namespace YakisobaGang.Script.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;
        [SerializeField] private bool disableDiagonalMovement = true;
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
            _rigidbody.AddForce(_myTransform.forward * speed, ForceMode.Impulse);
        }

        private void SelectMoveDirection(InputAction.CallbackContext ctx)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();
            
            // Olhar para frente
            if(direction == Vector2.up)
                _myTransform.rotation = Quaternion.identity;
            
            // Olhar para tras
            if (direction == Vector2.down)
                _myTransform.rotation = Quaternion.Euler(0, 180, 0);

            // Olhar para esquerda
            if (direction == Vector2.left)
                _myTransform.rotation = Quaternion.Euler(0, -90, 0);

            // Olhar para direita
            if (direction == Vector2.right)
                _myTransform.rotation = Quaternion.Euler(0, 90, 0);
        }

    }
}