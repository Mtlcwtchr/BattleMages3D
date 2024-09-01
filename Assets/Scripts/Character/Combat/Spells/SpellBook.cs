using System;
using System.Collections.Generic;
using System.Linq;

namespace Character.Combat.Spells
{
    public class SpellBook
    {
        public event Action<ISpell> OnSpellSelected;
        
        private List<ISpell> _knownSpells;

        private int _selectedIndex;
        private ISpell _selectedSpell;

        public ISpell SelectedSpell => _selectedSpell;

        public List<ISpell> KnownSpells => _knownSpells;

        public SpellBook(List<ISpell> knownSpells)
        {
            _knownSpells = knownSpells;
            _selectedIndex = 0;
            _selectedSpell = _knownSpells.FirstOrDefault();
        }

        public void SelectNext()
        {
            _selectedIndex = (_selectedIndex + 1) % _knownSpells.Count;
            _selectedSpell = _knownSpells[_selectedIndex];
            
            OnSpellSelected?.Invoke(_selectedSpell);
        }

        public void SelectPrevious()
        {
            _selectedIndex = (_selectedIndex - 1 + _knownSpells.Count) % _knownSpells.Count;
            _selectedSpell = _knownSpells[_selectedIndex];
            OnSpellSelected?.Invoke(_selectedSpell);
        }
    }
}