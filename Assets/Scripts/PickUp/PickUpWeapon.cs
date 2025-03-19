using SecondProject.Shooting;
using UnityEngine;

namespace SecondProject.PickUp
{
    [RequireComponent(typeof(MovingItem))]

    public class PickUpWeapon : PickUpItem
    {
        [SerializeField]
        [Tooltip("Префаб подбираемого оружия")]
        private Weapon _weaponPrefab;

        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetWeapon(_weaponPrefab);
        }
    }
}