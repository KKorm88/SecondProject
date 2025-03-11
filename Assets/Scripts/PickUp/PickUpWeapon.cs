using SecondProject.Shooting;
using UnityEngine;

namespace SecondProject.PickUp
{
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