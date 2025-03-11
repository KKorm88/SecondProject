using SecondProject.FSM;
using UnityEngine;

namespace SecondProject.Enemy.States
{
    public class RunawayState : BaseState
    {
        private readonly EnemyTarget _target;
        private readonly EnemyDirectionController _enemyDirectionController;
        private readonly EnemyCharacter _enemyCharacter;
        private readonly NavMesher _navMesher;
        private EnemyAccelerationController _enemyAccelerationController;

        private readonly float _healthThreshold;
        private readonly float _runawayDecisionChance;
        private readonly float _randomValue;
        private readonly float _accelerationCoefficient;
        private readonly float _maxSpeed;

        public RunawayState(EnemyTarget target, EnemyDirectionController enemyDirectionController, EnemyCharacter enemyCharacter,
            float healthThreshold, float runawayDecisionChance, NavMesher navMesher, EnemyAccelerationController enemyAccelerationController,
            float accelerationCoefficient, float maxSpeed)
        {
            _target = target;
            _enemyDirectionController = enemyDirectionController;
            _enemyCharacter = enemyCharacter;
            _navMesher = navMesher;//
            _healthThreshold = healthThreshold;
            _runawayDecisionChance = runawayDecisionChance;
            _randomValue = Random.value;
            _enemyAccelerationController = enemyAccelerationController;
            _accelerationCoefficient = accelerationCoefficient;
            _maxSpeed = maxSpeed;
        }

        public override void Execute()
        {
            //Debug.Log($"ПОБЕГ");
            if (_target == null || _target._player == null)
            {
                return;
            }

            float actualThreshold = (_healthThreshold / 100) * _enemyCharacter.MaxHealth;

            if (_enemyCharacter.CurrentHealth <= actualThreshold && _randomValue <= _runawayDecisionChance && _target.DistanceToPlayerFromAgent() <= _target._viewRadius)
            {
                _enemyAccelerationController.StartRun(_accelerationCoefficient, _maxSpeed);

                Vector3 targetPosition = _target._player.transform.position;
                Vector3 directionToTarget = (_enemyDirectionController.transform.position - targetPosition).normalized;
                Vector3 runawayPosition = _enemyDirectionController.transform.position + directionToTarget;

                _navMesher.CalculatePath(runawayPosition);

                if (_navMesher.IsPathCalculated)
                {
                    Vector3 nextPoint = _navMesher.GetCurrentPoint();
                    _enemyDirectionController.UpdateMovementDirection(nextPoint);
                }
            }
        }
    }
}
