using SecondProject.Enemy.States;
using UnityEngine;

namespace SecondProject.Enemy
{
    public class EnemyAiController : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Видимый радиус")]
        private float _viewRadius = 20f;

        [SerializeField]
        [Tooltip("Порог здоровья для перехода в состояние побега в процентах (от 0 до 100%)")]
        private float _healthThreshold = 30f;

        [SerializeField]
        [Tooltip("Вероятность решения убежать (значения от 0 до 1)")]
        private float _runawayDecisionChance = 0.7f;

        [SerializeField]
        [Tooltip("Коэффициент ускорения")]
        private float _accelerationCoefficient = 0.1f;

        [SerializeField]
        [Tooltip("Максимальная скорость")]
        private float _maxSpeed = 8f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var enemyAccelerationController = GetComponent<EnemyAccelerationController>();
            var navMesher = new NavMesher(transform);

            _target = new EnemyTarget(transform, player, _viewRadius);
            _stateMachine = new EnemyStateMachine(enemyDirectionController, navMesher, _target, _healthThreshold, 
                _runawayDecisionChance, enemyAccelerationController, _accelerationCoefficient, _maxSpeed);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();
        }
    }
}