using CameraLogic;
using Infrastructure;
using Services.Input;
using UnityEngine;

namespace Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private AnimationOperator _animationOperator;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start() => 
            _camera = Camera.main;
        
        private void Update()
        {
            _animationOperator.Idle();
            Vector3 movementVector = Vector3.zero;

            if(_inputService.Axis.sqrMagnitude != 0)
                _animationOperator.Run();

            if(_inputService.Axis.sqrMagnitude > .001f) //.001 нужны вынести в SO либо в класс с константами
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }
    }
}