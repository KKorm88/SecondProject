using SecondProject.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace SecondProject.Enemy.States
{
    public class EnemyStateMachine : BaseStateMachine
    {
        private const float NavMeshTurnOffDistance = 5;
        private EnemyCharacter _enemyCharacter;

        public EnemyStateMachine(EnemyDirectionController enemyDirectionController, 
            NavMesher navMesher, EnemyTarget target, float healthThreshold, float runawayDecisionChance, EnemyAccelerationController enemyAccelerationController, 
            float accelerationCoefficient, float maxSpeed)
        {
            _enemyCharacter = enemyDirectionController.GetComponent<EnemyCharacter>();

            var idleState = new IdleState(enemyAccelerationController);
            var findWayState = new FindWayState(target, navMesher, enemyDirectionController);
            var moveForwardState = new MoveForwardState(target, enemyDirectionController);
            var runawayState = new RunawayState(target, enemyDirectionController,
                 _enemyCharacter, healthThreshold, runawayDecisionChance, navMesher, enemyAccelerationController, accelerationCoefficient, maxSpeed);

            SetInitialState(idleState);

            AddState(state: idleState, transitions: new List<Transition>
                {
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                    new Transition(runawayState, () => (_enemyCharacter.CurrentHealth <= (healthThreshold / 100) *
                    _enemyCharacter.MaxHealth && Random.value <= runawayDecisionChance && target.DistanceToPlayerFromAgent() <= target._viewRadius))
            }
            );

            AddState(state: findWayState, transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null),
                    new Transition(
                        moveForwardState,
                        () => target.DistanceToClosestFromAgent() <= NavMeshTurnOffDistance),
                }
            );

            AddState(state: moveForwardState, transitions: new List<Transition>
                {
                    new Transition(
                        idleState,
                        () => target.Closest == null),
                    new Transition(
                        findWayState,
                        () => target.DistanceToClosestFromAgent() > NavMeshTurnOffDistance),
                    new Transition(runawayState, () => (_enemyCharacter.CurrentHealth <= (healthThreshold / 100) *
                    _enemyCharacter.MaxHealth && Random.value <= runawayDecisionChance && target.DistanceToPlayerFromAgent() <= target._viewRadius))
                }
            );

            AddState(runawayState, new List<Transition>
                {
                    new Transition(idleState, () => target.DistanceToPlayerFromAgent() > target._viewRadius)
                }
            );
        }
    }
}
