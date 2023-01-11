using Data;
using Infrastructure.Services;
using Services.Input;
using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private AnimationOperator _animationOperator;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();

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

        public void UpdateProgress(PlayerProgress progress) => 
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());

        public void LoadProgress(PlayerProgress progress)
        {
            if(CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if(savedPosition != null)
                    Warp(savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}