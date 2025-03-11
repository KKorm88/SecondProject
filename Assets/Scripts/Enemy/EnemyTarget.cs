using UnityEngine;

namespace SecondProject.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest { get; private set; }
        public GameObject TargetWeapon { get; private set; }

        public readonly float _viewRadius;
        private readonly Transform _agentTransform;
        public PlayerCharacter _player;

        private readonly Collider[] _colliders = new Collider[10];

        public EnemyTarget(Transform agent, PlayerCharacter player, float viewRadius)
        {
            _agentTransform = agent;
            _player = player;
            _viewRadius = viewRadius;
        }

        public void FindClosest()
        {
            float minDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.PickUpsMask | LayerUtils.PlayerAndEnemyMask);

            BaseCharacter character = _agentTransform.GetComponent<BaseCharacter>();
            bool hasNewWeapon = character != null && character.HasPickedUpNewWeapon();//

            TargetWeapon = null;

            for (int i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;
                if (go == _agentTransform.gameObject) 
                    continue;

                var distance = DistanceFromAgentTo(go);
                bool isPickUp = LayerUtils.IsPickUp(go);

                if (isPickUp && !hasNewWeapon)
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        TargetWeapon = go;
                    }
                }
                else if (!isPickUp)
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        Closest = go;
                    }
                }
            }

            if (_player != null && DistanceFromAgentTo(_player.gameObject) < minDistance)
            {
                Closest = _player.gameObject;
            }

            if (TargetWeapon != null)
            {
                Closest = TargetWeapon;
            }
        }

        public float DistanceToClosestFromAgent()
        {
            if (Closest != null)
                return DistanceFromAgentTo(Closest);

            return 0;
        }

        public float DistanceToPlayerFromAgent()
        {
            FindClosest();

            if (_player != null)
            {
                return DistanceFromAgentTo(_player.gameObject);
            }

            return 0;
        }

        private int FindAllTargets(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(
                _agentTransform.position,
                _viewRadius,
                _colliders,
                layerMask);

            return size;
        }

        private float DistanceFromAgentTo(GameObject go) => (_agentTransform.position - go.transform.position).magnitude;
    }
}
