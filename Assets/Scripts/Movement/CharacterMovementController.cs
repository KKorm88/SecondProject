using UnityEngine;

namespace SecondProject.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        private float _speed = 1f;//Скорость передвижения
        [SerializeField]
        private float _maxRadiansDelta = 10f;//Скорость поворота


        [SerializeField]//
        private float _accelerationCoefficient = 2f;//Коэффициент ускорения
        [SerializeField]//
        private float _maxSpeed = 8f;//Максимальная скорость


        public Vector3 MovementDirection { get; set; }//Устанавливается направление движения из IMovementDirectionSource (интерфейс), а PlayerMovementController - класс, реализация интерфейса
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;


        private float _currentSpeed;//Текущая скорость


        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _currentSpeed = _speed;//Смотрим текущую скорость
        }

        protected void Update()
        {
            InputForAcceleration();//Смотрим есть ли ускорение


            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }


        private void InputForAcceleration()
        {
            if (Input.GetKey(KeyCode.Space))//Смотрим нажат ли пробел
            {
                _currentSpeed += _accelerationCoefficient * Time.deltaTime;//Увеличиваем скорость
                if (_currentSpeed > _maxSpeed)//Ограничиваем скорость
                    _currentSpeed = _maxSpeed;
            }
            else
            {
                _currentSpeed = _speed;//Сбрасываем скорость
            }
        }    

        private void Translate()
        {
            var delta = MovementDirection * _currentSpeed * Time.deltaTime;//Дельта, на которую передвигаемся за кадр
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var currentLookDirection = transform.rotation * Vector3.forward;
            float sqrMagnitude = (currentLookDirection - LookDirection).sqrMagnitude;

            if (sqrMagnitude > SqrEpsilon)
            {
                var newRotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(LookDirection, Vector3.up),
                    _maxRadiansDelta * Time.deltaTime);

                transform.rotation = newRotation;
            }
        }
    }
}