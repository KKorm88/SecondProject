using SecondProject.Movement;
using UnityEngine;

namespace SecondProject.Enemy
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class EnemyAccelerationController : MonoBehaviour
    {
        private float _accelerationCoefficient;
        private float _maxSpeed;
        private float _newSpeed;
        
        private CharacterMovementController _characterMovementController;

        protected void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
        }

        public void StartRun(float accelerationCoefficient, float maxSpeed)
        {
            _accelerationCoefficient = accelerationCoefficient;
            _maxSpeed = maxSpeed;

            _newSpeed = _characterMovementController.CurrentSpeed + _accelerationCoefficient * Time.deltaTime;

            if (_newSpeed > _maxSpeed)
                _newSpeed = _maxSpeed;

            _characterMovementController.SetSpeed(_newSpeed);
        }

        public void StopRun()
        {
            _characterMovementController.SetSpeed(_characterMovementController.InitialSpeed);
        }
    }
}