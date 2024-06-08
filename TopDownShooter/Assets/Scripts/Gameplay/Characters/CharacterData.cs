using UnityEngine;

namespace Gameplay.Characters
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "TopDownShooter/CharacterData")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterViewBase _characterViewPrefab;
        public CharacterViewBase CharacterViewPrefab => _characterViewPrefab;
    }
}