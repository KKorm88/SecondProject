using UnityEditor;
using UnityEngine;

namespace SecondProject.PickUp
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Префаб спавн")]
        private PickUpItem _pickUpPrefab;

        [SerializeField]
        [Tooltip("Радиус спавн-зоны")]
        private float _range = 2f;

        [SerializeField]
        [Tooltip("Максимальное количество предметов, которые могут быть заспавнены внутри зоны")]
        private int _maxCount = 2;

        [SerializeField]
        [Tooltip("Минимальное время до следующего спавна предмета")]
        private float _minSpawnIntervalSeconds = 2f;

        [SerializeField]
        [Tooltip("Максимальное время до следующего спавна предмета")]
        private float _maxSpawnIntervalSeconds = 10f;
        
        private float _currentSpawnTimerSeconds;
        private int _currentCount;

        private float _currentSpawnIntervalSeconds;
        
        protected void Start()
        {
            SetRandomSpawnInterval();
        }

        protected void Update()
        {
            if (_currentCount < _maxCount)
            {
                _currentSpawnTimerSeconds += Time.deltaTime;

                if (_currentSpawnTimerSeconds > _currentSpawnIntervalSeconds)
                {
                    _currentSpawnTimerSeconds = 0f;
                    _currentCount++;

                    var randomPointInsideRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + 
                        transform.position;

                    var pickUp = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity, transform);
                    pickUp.OnPickedUp += OnItemPickedUp;

                    SetRandomSpawnInterval();
                }
            }
        }

        protected void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _currentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }

        private void SetRandomSpawnInterval()
        {
            _currentSpawnIntervalSeconds = Random.Range(_minSpawnIntervalSeconds, _maxSpawnIntervalSeconds);
        }
    }
}