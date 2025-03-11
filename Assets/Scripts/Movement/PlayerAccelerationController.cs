using SecondProject.AccelerationBonus;
using UnityEngine;

namespace SecondProject.Movement
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class PlayerAccelerationController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Коэффициент ускорения")]
        private float _accelerationCoefficient = 2f;

        [SerializeField]
        [Tooltip("Максимальная скорость")]
        private float _maxSpeed = 8f;

        private CharacterMovementController _characterMovementController;
        private bool _isAccelerating;
        private SpeedBoosterController _speedBoosterController;

        protected void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
            _speedBoosterController = GetComponent<SpeedBoosterController>();
        }

        protected void Update()
        {
            InputForAcceleration();
        }

        private void InputForAcceleration()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _isAccelerating = true;
            }
            else
            {
                if (!_speedBoosterController.IsActive())
                {
                    _isAccelerating = false;
                    _characterMovementController.SetSpeed(_characterMovementController.InitialSpeed);
                }
            }

            if (_isAccelerating)
            {
                float newSpeed = _characterMovementController.CurrentSpeed + _accelerationCoefficient * Time.deltaTime;
                if (newSpeed > _maxSpeed)
                    newSpeed = _maxSpeed;

                _characterMovementController.SetSpeed(newSpeed);
            }
        }

        public float GetSpeedMultiplier()
        {
            return _accelerationCoefficient;
        }

        public bool IsActive()
        {
            return _isAccelerating;
        }
    }
}