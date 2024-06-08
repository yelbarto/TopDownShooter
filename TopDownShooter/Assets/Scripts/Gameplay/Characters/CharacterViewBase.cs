using UnityEngine;

namespace Gameplay.Characters
{
    public class CharacterViewBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private BasicFillbarComponent _healthBar;
        [SerializeField] private BasicFillbarComponent _armorBar;
        [SerializeField] private Transform _weaponHolder;
        public Transform WeaponHolder => _weaponHolder;
        public OnDamageReceivedDelegate OnDamageReceived { get; set; }
        
        private void Awake()
        {
            OnAwake();
        }

        public void Reinitialize(Vector3 position)
        {
            transform.position = position;
            UpdateHealthArea(1, 1);
        }

        public void UpdateHealthArea(float healthPercentage, float armorPercentage)
        {
            _healthBar.UpdateFillbar(healthPercentage);
            _armorBar.UpdateFillbar(armorPercentage);
        }

        protected virtual void OnAwake()
        {
            
        }
    }
}