using UnityEngine;

namespace SecondProject.PickUp
{
    public class BoosterPickUp : PickUpItem
    {
        [SerializeField]
        [Tooltip("Длительность ускорения в секундах")]
        private float _duration = 5f;

        [SerializeField]
        [Tooltip("Коэффициент ускорения при использовании бустера")]
        private float _accelerationCoefficient = 2f;

        [SerializeField]
        [Tooltip("Максимальная скорость при использовании бустера")]
        private float _maxSpeed = 8f;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.ActivateBooster(_duration, _accelerationCoefficient, _maxSpeed);
        }
    }
}