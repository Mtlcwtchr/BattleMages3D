using System;
using UnityEngine;

namespace Character
{
    public class CharacterData
    {
        public event Action<float> OnHpChanged;
        
        public CharacterConfig Config { get; }
        
        public float MaxHp { get; }
        public float Hp
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Clamp(value, 0, MaxHp);
                OnHpChanged?.Invoke(_currentHp);
            }
        }
        
        public float Armor { get; }
        
        public float MovementSpeed { get; }
        
        private float _currentHp;

        public CharacterData(CharacterConfig config)
        {
            Config = config;
            MaxHp = config.maxHp;
            Armor = config.armor;
            MovementSpeed = config.movementSpeed;
            Hp = MaxHp;
        }
    }
}