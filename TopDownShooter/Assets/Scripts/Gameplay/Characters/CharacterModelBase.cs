using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Inventories.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gameplay.Characters
{
    public class CharacterModelBase : IDisposable
    {
        protected WeaponBaseModel[] Weapons { get; set; }
        private int Health { get; set; }
        private int Armor { get; set; }

        protected readonly CharacterViewBase CharacterView;
        protected int WeaponIndex;
        
        private readonly CancellationTokenSource _lifetimeCts;
        private readonly WeaponFactory _weaponFactory;
        private readonly Vector3 _initialPosition;
        private readonly int _initialHealth;
        private readonly int _initialArmor;

        public CharacterModelBase(CharacterData characterData, Vector3 originPoint, Transform parent, 
            string name, WeaponFactory weaponFactory)
        {
            _initialHealth = 100;
            _initialArmor = 100;
            _initialPosition = originPoint;
            _weaponFactory = weaponFactory;
            Health = _initialHealth;
            Armor = _initialArmor;
            CharacterView = Object.Instantiate(characterData.CharacterViewPrefab, originPoint, Quaternion.identity,
                parent);
            CharacterView.name = name;
            CharacterView.OnDamageReceived += OnDamageReceived;
            _lifetimeCts = new CancellationTokenSource();
        }

        protected virtual void SetUpViewEvents()
        {
            CharacterView.OnFire += Fire;
        }

        protected WeaponBaseModel CreateWeapon(WeaponType weaponType)
        {
            return _weaponFactory.CreateObject(weaponType, CharacterView.WeaponHolder);
        }

        private void OnDamageReceived(int damage, int armorPiercing)
        {
            Debug.Log($"On Damage Received Before: Id {CharacterView.name}, Health: {Health}, Armor: {Armor}");
            if (Armor > 0)
            {
                Armor -= damage - armorPiercing;
                if (Armor < 0)
                {
                    Health += Armor;
                    Armor = 0;
                }
                damage -= damage - armorPiercing;
            }
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                KillAndRespawn_Async().Forget();
            }
            Debug.Log($"On Damage Received After: Id {CharacterView.name}, Health: {Health}, Armor: {Armor}");
            CharacterView.UpdateHealthArea((float) Health / _initialHealth, (float) Armor / _initialArmor);
        }

        private async UniTask KillAndRespawn_Async()
        {
            CharacterView.Kill();
            await WaitForRespawn();
            Health = _initialHealth;
            Armor = _initialArmor;
            CharacterView.Reinitialize(_initialPosition);
        }
        
        private void Fire()
        {
            Weapons[WeaponIndex].Fire(CharacterView);
        }

        protected virtual async UniTask WaitForRespawn()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: _lifetimeCts.Token);
        }
        
        public virtual void Dispose()
        {
            CharacterView.OnDamageReceived -= OnDamageReceived;
            CharacterView.OnFire -= Fire;
        }
    }
}
