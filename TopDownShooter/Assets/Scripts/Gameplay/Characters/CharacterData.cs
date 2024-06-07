using UnityEngine;

namespace Gameplay.Characters
{
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private CharacterViewBase _characterViewPrefab;
        public CharacterViewBase CharacterViewPrefab => _characterViewPrefab;
    }
}