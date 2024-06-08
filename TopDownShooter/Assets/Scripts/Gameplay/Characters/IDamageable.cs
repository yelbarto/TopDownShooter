namespace Gameplay.Characters
{
    public interface IDamageable
    {
        OnDamageReceivedDelegate OnDamageReceived { get; }
        CharacterType CharacterType { get; }
    }

    public enum CharacterType
    {
        Player, Enemy
    }
    
    public delegate void OnDamageReceivedDelegate(int damage, int armorPiercing);
}