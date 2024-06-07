using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Characters
{
    public class CharacterViewBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _armorBar;
        
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
            _healthBar.fillAmount = healthPercentage;
            _armorBar.fillAmount = armorPercentage;
        }

        protected virtual void OnAwake()
        {
            
        }
    }
}