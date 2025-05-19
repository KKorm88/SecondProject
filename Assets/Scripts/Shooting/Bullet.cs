using UnityEngine;

namespace SecondProject.Shooting
{
    public class Bullet : MonoBehaviour
    {
        public float Damage { get; private set; }

        public GameObject Owner { get; private set; }

        private Vector3 _direction;
        private float _flySpeed;
        private float _maxFlyDistance;
        private float _currentFlyDistance;

        public void Initialize(Vector3 direction, float maxFlyDistance, float flySpeed, float damage, GameObject owner)
        {
            _direction = direction;
            _maxFlyDistance = maxFlyDistance;
            _flySpeed = flySpeed;
            Damage = damage;
            Owner = owner;
        }

        protected void Update()
        {
            var delta = _flySpeed * Time.deltaTime;
            _currentFlyDistance += delta;
            transform.Translate(_direction * delta);

            if (_currentFlyDistance >= _maxFlyDistance)
                Destroy(gameObject);
        }

    }
}
