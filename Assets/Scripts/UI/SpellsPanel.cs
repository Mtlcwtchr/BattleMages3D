using System;
using System.Collections.Generic;
using Character.Combat.Spells;
using Character.Combat.Spells.Config;
using UnityEngine;

namespace UI
{
    public class SpellsPanel : MonoBehaviour
    {
        [SerializeField] private SpellElement template;
        [SerializeField] private Transform root;

        private Dictionary<ESpell, SpellElement> _elements;

        private SpellBook _spellBook;

        private SpellElement _selected;

        private void Awake()
        {
            _elements = new();
        }

        private void OnDestroy()
        {
            Clear();
        }

        public void SetData(SpellBook spellBook)
        {
            Clear();
            _spellBook = spellBook;
            _spellBook.OnSpellSelected += SpellSelected;
            
            for (var i = 0; i < _spellBook.KnownSpells.Count; i++)
            {
                AddSpell(_spellBook.KnownSpells[i].Config);
            }

            SpellSelected(_spellBook.SelectedSpell);
        }

        private void Clear()
        {
            if (_spellBook != null)
            {
                _spellBook.OnSpellSelected -= SpellSelected;
            }
            foreach (var (type, element) in _elements)
            {
                Destroy(element.gameObject);
            }
            _elements.Clear();
        }

        private void AddSpell(SpellConfig config)
        {
            var element = Instantiate(template, root);
            element.SetContent(config);
            _elements.Add(config.type, element);
        }

        private void SpellSelected(ISpell spell)
        {
            if (_selected != null)
            {
                _selected.SetSelected(false);
            }
            
            if (_elements.TryGetValue(spell.Config.type, out var element))
            {
                _selected = element;
                element.SetSelected(true);
            }
        }
    }
}