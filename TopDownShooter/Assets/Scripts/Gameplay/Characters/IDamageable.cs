using System;

namespace Gameplay.Characters
{
    public interface IDamageable
    {
        OnDamageReceivedDelegate OnDamageReceived { get; set; }
    }
    
    public delegate void OnDamageReceivedDelegate(int damage, int armorPiercing);
}