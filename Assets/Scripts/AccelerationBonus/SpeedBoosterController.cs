using SecondProject.Movement;
using UnityEngine;

namespace SecondProject.AccelerationBonus
{
    [RequireComponent(typeof(CharacterMovementController))]
    public class SpeedBoosterController : MonoBehaviour
    {
        private float _accelerationCoefficient;
        private float _maxSpeed;
        private CharacterMovementController _characterMovementController;
        private float _boosterEffectEndTime;
        private bool _isActive;

        [SerializeField]
        [Tooltip("Эффект частиц бустера")]
        private ParticleSystem _boosterParticles;

        private void Awake()
        {
            _characterMovementController = GetComponent<CharacterMovementController>();

            _boosterParticles.Stop();
        }

        private void Update()
        {
            if (_isActive && Time.time < _boosterEffectEndTime)
            {
                float newSpeed = _characterMovementController.CurrentSpeed + _accelerationCoefficient * Time.deltaTime;
                if (newSpeed > _maxSpeed)
                {
                    newSpeed = _maxSpeed;
                }
                _characterMovementController.SetSpeed(newSpeed);
            }
            else if (_isActive && Time.time > _boosterEffectEndTime)
            {
                DisableBooster();
            }
        }

        public void EnableBooster(float duration, float accelerationCoefficient, float maxSpeed)
        {
            if (_isActive)
            {
                return;
            }

            _boosterEffectEndTime = Time.time + duration;
            _accelerationCoefficient = accelerationCoefficient;
            _maxSpeed = maxSpeed;
            _isActive = true;

            _boosterParticles.Play();
        }

        private void DisableBooster()
        {
            _characterMovementController.SetSpeed(_characterMovementController.InitialSpeed);
            _isActive = false;

            _boosterParticles.Stop();
        }

        public float GetSpeedMultiplier()
        {
            return _accelerationCoefficient;
        }

        public bool IsActive()
        {
            return _isActive;
        }
    }
}