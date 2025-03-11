using SecondProject.AccelerationBonus;
using SecondProject.Movement;
using SecondProject.PickUp;
using SecondProject.Shooting;
using UnityEngine;

namespace SecondProject
{
    [RequireComponent(typeof(CharacterMovementController), typeof(ShootingController), typeof(SpeedBoosterController))]

    public abstract class BaseCharacter : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Префаб «базового» оружия")]
        private Weapon _baseWeaponPrefab;

        [SerializeField]
        [Tooltip("Точка «руки», куда будет помещено оружие")]
        private Transform _hand;

        [SerializeField]
        [Tooltip("Значение жизни")]
        private float _health = 2f;

        private float _maxHealth;
        private Weapon _currentWeapon;
        public float CurrentHealth => _health;
        public float MaxHealth => _maxHealth;
        public Weapon CurrentWeapon => _currentWeapon;

        private IMovementDirectionSource _movementDirectionSource;
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
        private SpeedBoosterController _speedBoosterController;
        private CharacterSpawner _characterSpawner;

        protected void Awake()
        {
            _maxHealth = _health;

            _movementDirectionSource = GetComponent<IMovementDirectionSource>();
            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
            _speedBoosterController = GetComponent<SpeedBoosterController>();
            _characterSpawner = FindObjectOfType<CharacterSpawner>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeaponPrefab);
        }

        protected void Update()
        {
            var direction = _movementDirectionSource.MovementDirection;
            var lookDirection = direction;
            if (_shootingController.HasTarget)
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;

            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            if (_health <= 0f)
            {
                if (LayerUtils.IsPlayer(gameObject))
                {
                    PlayerAndEnemyStatus.StatusPlayerSpawned = false;
                }
                if (LayerUtils.IsEnemy(gameObject))
                {
                    PlayerAndEnemyStatus.StatusEnemySpawned = false;
                    if (_characterSpawner != null)
                    {
                        _characterSpawner.DecreaseEnemyCount();
                    }
                }
                Destroy(gameObject);
            }

        }

        protected void OnTriggerEnter(Collider other)
        {
            if (LayerUtils.IsBullet(other.gameObject))
            {
                var bullet = other.gameObject.GetComponent<Bullet>();

                _health -= bullet.Damage;

                Destroy(other.gameObject);
            }
            else if (LayerUtils.IsPickUp(other.gameObject))
            {
                var pickUp = other.gameObject.GetComponent<PickUpItem>();
                if (pickUp != null)
                {
                    pickUp.PickUp(this);
                }

                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            _shootingController.SetWeapon(weapon, _hand);
        }

        public bool HasPickedUpNewWeapon()
        {
            return _currentWeapon != _baseWeaponPrefab;
        }

        public void ActivateBooster(float duration, float accelerationCoefficient, float maxSpeed)
        {
            _speedBoosterController.EnableBooster(duration, accelerationCoefficient, maxSpeed);
        }
    } 
}
