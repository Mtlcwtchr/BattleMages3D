using System;
using System.Collections.Generic;
using System.Linq;

namespace Character.Combat.Spells
{
    public class SpellBook
    {
        public event Action<ISpell> OnSpellSelected;

        public List<ISpell> KnownSpells { get; }
        public ISpell SelectedSpell { get; private set; }
        
        private int _selectedIndex;

        public SpellBook(List<ISpell> knownSpells)
        {
            KnownSpells = knownSpells;
            _selectedIndex = 0;
            SelectedSpell = KnownSpells.FirstOrDefault();
        }

        public void SelectNext()
        {
            _selectedIndex = (_selectedIndex + 1) % KnownSpells.Count;
            SelectedSpell = KnownSpells[_selectedIndex];
            
            OnSpellSelected?.Invoke(SelectedSpell);
        }

        public void SelectPrevious()
        {
            _selectedIndex = (_selectedIndex - 1 + KnownSpells.Count) % KnownSpells.Count;
            SelectedSpell = KnownSpells[_selectedIndex];
            OnSpellSelected?.Invoke(SelectedSpell);
        }
    }
}