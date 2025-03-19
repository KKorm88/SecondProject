using UnityEngine;

namespace SecondProject.PickUp
{
    public class MovingItem : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Скорость вращения")]
        private float _rotationSpeed = 100f;

        [SerializeField]
        [Tooltip("Высота плавания")]
        private float _swimHeight = 0.5f;

        [SerializeField]
        [Tooltip("Скорость плавания")]
        private float _swimSpeed = 2f;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);

            float swim = startPosition.y + Mathf.Sin(Time.time * _swimSpeed) * _swimHeight;
            transform.position = new Vector3(startPosition.x, swim, startPosition.z);
        }
    }
}