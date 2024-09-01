using System;
using System.Collections.Generic;
using Character.Combat.Spells;
using UnityEngine;

namespace Character
{
    public class CharacterModel
    {
        public event Action OnCharacterDied;
        public event Action<float> OnHpChanged;
        
        public SpellBook SpellBook { get; private set; }
        
        public CharacterData Data { get; }

        public IDamageReceiver DamageReceiver { get; }

        public Vector3 Position { get; private set; }
        public Vector2 AbsoluteDirection { get; private set; }
        public Vector3 AttackDirection { get; private set; }
        public float SpeedNormalized { get; private set; }
        
        public bool IsVisible { get; set; }
        
        public CharacterController View { get; private set; }

        public CharacterModel(CharacterConfig config)
        {
            Data = new CharacterData(config);

            var spells = new List<ISpell>();
            for (var i = 0; i < config.spells.Count; i++)
            {
                var spell = SpellFactory.Create(config.spells[i]);
                spells.Add(spell);
            }

            SpellBook = new SpellBook(spells);

            var armorReceiver = new ArmorDamageReceiver(Data.Armor);
            var hpReceiver = new HealthDamageReceiver(Data);
            armorReceiver.SetNext(hpReceiver);
            DamageReceiver = armorReceiver;

            IsVisible = true;
            
            Data.OnHpChanged += HpChanged;
        }

        public void Init(CharacterController view)
        {
            View = view;
        }

        public void SetViewData(Vector3 position, Vector2 absoluteDirection, Vector3 attackDirection, float speedNormalized)
        {
            Position = position;
            AbsoluteDirection = absoluteDirection;
            AttackDirection = attackDirection;
            SpeedNormalized = speedNormalized;
        }

        private void HpChanged(float obj)
        {
            OnHpChanged?.Invoke(obj);
            if (obj <= 0)
            {
                OnCharacterDied?.Invoke();
            }
        }
    }
}