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

        private float _newSpeed;

        private CharacterMovementController _characterMovementController;
        
        protected void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();
        }

        protected void Update()
        {
            InputForAcceleration();
        }

        private void InputForAcceleration()
        {
            //Смотрим нажат ли пробел
            if (Input.GetKey(KeyCode.Space))
            {
                //Увеличиваем скорость
                _newSpeed = _characterMovementController.CurrentSpeed + _accelerationCoefficient * Time.deltaTime;
                //Ограничиваем скорость
                if (_newSpeed > _maxSpeed)
                    _newSpeed = _maxSpeed;

                _characterMovementController.SetSpeed(_newSpeed);
            }
            else
            {
                //Сбрасываем скорость
                _characterMovementController.SetSpeed(_characterMovementController.InitialSpeed);
            }
        }
    }
}