using Gameplay.Inventories.Weapons;
using UnityEngine;

namespace Gameplay.Characters.Enemy
{
    public class EnemyModel : CharacterModelBase
    {
        public EnemyModel(CharacterData characterData, Vector3 originPoint, Transform parent, string name,
            WeaponFactory weaponFactory) : base(characterData, originPoint, parent, name, weaponFactory)
        {
            Weapons = new WeaponBaseModel[1];
            Weapons[0] = CreateWeapon((WeaponType) Random.Range(0, 3));
            Weapons[0].SwitchWeapon(true);
        }
    }
}