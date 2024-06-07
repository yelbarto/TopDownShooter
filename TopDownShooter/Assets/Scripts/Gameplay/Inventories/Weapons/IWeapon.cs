namespace Gameplay.Inventories.Weapons
{
    public interface IWeapon
    {
        public int Damage { get; set; }
        public int Range { get; set; }
        public int ArmorPiercingPercentage { get; set; }
    }
}