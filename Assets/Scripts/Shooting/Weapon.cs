using UnityEngine;

namespace SecondProject.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField]
        public Bullet BulletPrefab { get; private set; }
        [field: SerializeField]
        [Tooltip("Радиус стрельбы")]
        public float ShootRadius { get; private set; } = 5f;
        [field: SerializeField]
        [Tooltip("Частота выстрелов")]
        public float ShootFrequencySec { get; private set; } = 1f;

        [SerializeField]
        [Tooltip("Урон от выстрела")]
        private float _damage = 1f;

        [SerializeField]
        [Tooltip("Максимальная дистанция полета пули")]
        private float _bulletMaxFlyDistance = 10f;

        [SerializeField]
        [Tooltip("Скорость полета пули")]
        private float _bulletFlySpeed = 10f;

        [SerializeField]
        [Tooltip("Точка «спавна» пули из оружия")]
        private Transform _bulletSpawnPosition;

        public void Shoot(Vector3 targetPoint)
        {
            var bullet = Instantiate(BulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);

            var target = targetPoint - _bulletSpawnPosition.position;
            target.y = 0;
            target.Normalize();

            bullet.Initialize(target, _bulletMaxFlyDistance, _bulletFlySpeed, _damage);
        }
    }
}