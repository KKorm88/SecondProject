using SecondProject.Camera;
using UnityEditor;
using UnityEngine;

namespace SecondProject.PickUp
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Префаб игрока")]
        private GameObject _playerPrefab;

        [SerializeField]
        [Tooltip("Префаб врага")]
        private GameObject _enemyPrefab;

        [SerializeField]
        [Tooltip("Радиус спавн-зоны")]
        private float _range = 2f;

        [SerializeField]
        [Tooltip("Максимальное количество врагов, которые могут быть заспавнены внутри зоны")]
        private int _maxEnemyCount = 5;

        [SerializeField]
        [Tooltip("Минимальное время до следующего спавна")]
        private float _minSpawnIntervalSeconds = 2f;

        [SerializeField]
        [Tooltip("Максимальное время до следующего спавна")]
        private float _maxSpawnIntervalSeconds = 10f;

        private CameraController _cameraController;
        private float _currentSpawnIntervalSeconds;
        private float _currentSpawnTimerSeconds;
        private int _currentCountEnemy;

        void Start()
        {
            _cameraController = FindObjectOfType<CameraController>();
            SetRandomSpawnInterval();
        }

        void Update()
        {
            _currentSpawnTimerSeconds += Time.deltaTime;

            if (_currentSpawnTimerSeconds > _currentSpawnIntervalSeconds)
            {
                _currentSpawnTimerSeconds = 0f;

                if (!PlayerAndEnemyStatus.StatusPlayerSpawned && Random.value < 0.5f)
                {
                    SpawnPlayer();
                    PlayerAndEnemyStatus.StatusPlayerSpawned = true;
                }
                else if (_currentCountEnemy < _maxEnemyCount)
                {
                    SpawnEnemy();
                    _currentCountEnemy++;
                }
                else if (_currentCountEnemy == _maxEnemyCount)
                {
                    PlayerAndEnemyStatus.StatusEnemySpawned = true;
                }

                SetRandomSpawnInterval();
            }
        }


        private void SetRandomSpawnInterval()
        {
            _currentSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }

        private void SpawnPlayer()
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

            GameObject playerObject = Instantiate(_playerPrefab, randomPosition, Quaternion.identity, transform);

            PlayerCharacter playerCharacter = playerObject.GetComponent<PlayerCharacter>();
            if (playerCharacter != null && _cameraController != null)
            {
                _cameraController.AssignPlayer(playerCharacter);
            }
        }

        private void SpawnEnemy()
        {
            var randomPointInsideRange = Random.insideUnitSphere * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.z) + transform.position;

            Instantiate(_enemyPrefab, randomPosition, Quaternion.identity, transform);
        }

        protected void OnDrawGizmos()
        {
            var cachedColor = Handles.color;
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cachedColor;
        }

        public void DecreaseEnemyCount()
        {
                _currentCountEnemy--;
        }
    }
}