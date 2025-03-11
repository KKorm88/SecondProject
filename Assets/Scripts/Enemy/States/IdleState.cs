using SecondProject.FSM;

namespace SecondProject.Enemy.States
{
    public class IdleState : BaseState
    {
        private EnemyAccelerationController _enemyAccelerationController;

        public IdleState(EnemyAccelerationController enemyAccelerationController)
        {
            _enemyAccelerationController = enemyAccelerationController;
        }

        public override void Execute()
        {
            //Debug.Log($"ОЖИДАНИЕ");
            _enemyAccelerationController.StopRun();
        }
    }
}
