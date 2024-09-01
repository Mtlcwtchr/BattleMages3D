using Character.Combat.Spells;

namespace Character.Combat
{
    public class MagicAttackStrategy : AttackStrategy
    {
        private SpellBook _spellBook;
        
        public ISpell CurrentSpell { get; set; }

        private ISpell _selectedSpell;
        
        private bool _attackInProgress;

        public void SetupSpellBook(SpellBook spellBook)
        {
            _spellBook = spellBook;
            CurrentSpell = _spellBook.SelectedSpell;
            
            spellBook.OnSpellSelected += SpellSelected;

            void SpellSelected(ISpell spell)
            {
                _selectedSpell = spell;
                if(!_attackInProgress)
                {
                    UpdateCurrentSpell();
                }
            }
        }

        private void UpdateCurrentSpell()
        {
            if (_selectedSpell != null)
            {
                CurrentSpell = _selectedSpell;
                _selectedSpell = null;
            }
        }
        
        protected override void AttackFinish()
        {
            _attackInProgress = false;
            CurrentSpell.EndCast(character);
            
            UpdateCurrentSpell();
        }

        protected override void AttackStart()
        {
            _attackInProgress = true;
            CurrentSpell.BeginCast(character);
        }

        protected override bool CanAttack()
        {
            return !_attackInProgress && CurrentSpell != null;
        }
    }
}