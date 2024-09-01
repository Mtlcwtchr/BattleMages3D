using System;
using System.Collections.Generic;
using Character.Combat.Spells;
using UnityEngine;

namespace Character.Entity
{
    public class Entity
    {
        public EntityConfig Config { get; private set; }
        
        public event Action<float> OnHpChanged;
        
        private float _maxHp;
        private float _currentHp;

        public float MaxHp => _maxHp;
        
        public float Hp
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Clamp(value, 0, _maxHp);
                OnHpChanged?.Invoke(_currentHp);
            }
        }
        
        public float Armor { get; set; }
        
        public float MovementSpeed { get; set; }

        public SpellBook SpellBook { get; private set; }
        
        public Entity(EntityConfig config)
        {
            Config = config;
            _maxHp = config.maxHp;
            Armor = config.armor;
            MovementSpeed = config.movementSpeed;

            var spells = new List<ISpell>();
            for (var i = 0; i < config.spells.Count; i++)
            {
                var spell = SpellFactory.Create(config.spells[i]);
                if (spell != null)
                {
                    spells.Add(spell);
                }
            }

            SpellBook = new SpellBook(spells);
            Refill();
        }

        public void Refill()
        {
            Hp = _maxHp;
        }
    }
}