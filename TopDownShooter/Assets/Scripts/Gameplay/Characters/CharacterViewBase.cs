using System;
using UnityEngine;

namespace Gameplay.Characters
{
    public class CharacterViewBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private BasicFillbarComponent _healthBar;
        [SerializeField] private BasicFillbarComponent _armorBar;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private GameObject _physicalBody;
        [SerializeField] private GameObject _uiArea;
        public Transform WeaponHolder => _weaponHolder;
        public OnDamageReceivedDelegate OnDamageReceived { get; set; }
        public Action OnFire;
        public CharacterType CharacterType { get; protected set; }
        
        private void Awake()
        {
            OnAwake();
        }

        public virtual void Kill()
        {
            ChangeVisibility(false);
        }

        private void ChangeVisibility(bool isVisible)
        {
            _physicalBody.SetActive(isVisible);
            _uiArea.SetActive(isVisible);
        }

        public virtual void Reinitialize(Vector3 position)
        {
            transform.position = position;
            ChangeVisibility(true);
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