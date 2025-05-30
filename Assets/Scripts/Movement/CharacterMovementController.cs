﻿using UnityEngine;

namespace SecondProject.Movement
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovementController : MonoBehaviour
    {
        private static readonly float SqrEpsilon = Mathf.Epsilon * Mathf.Epsilon;

        [SerializeField]
        [Tooltip("Скорость передвижения")]
        private float _speed = 1f;
        [SerializeField]
        [Tooltip("Скорость поворота при передвижении")]
        private float _maxRadiansDelta = 10f;

        //Устанавливается направление движения из IMovementDirectionSource (интерфейс), а PlayerMovementController - класс, реализация интерфейса
        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }

        private CharacterController _characterController;
        private float _currentSpeed;

        public float CurrentSpeed => _currentSpeed;
        public float InitialSpeed => _speed;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _currentSpeed = _speed;
        }

        protected void Update()
        {
            Translate();

            if (_maxRadiansDelta > 0f && LookDirection != Vector3.zero)
                Rotate();
        }
        
        public void SetSpeed(float speed)
        {
            _currentSpeed = speed;
        }

        private void Translate()
        {
            float totalSpeed = _currentSpeed;
            var speedModifiers = GetComponents<ISpeedModifier>();
            foreach (var modifier in speedModifiers)
            {
                if (modifier.IsActive())
                {
                    totalSpeed += modifier.GetSpeedMultiplier();
                }
            }

            var delta = MovementDirection * totalSpeed * Time.deltaTime;
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