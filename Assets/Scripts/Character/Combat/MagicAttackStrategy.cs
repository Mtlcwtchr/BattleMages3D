using Character.Combat.Spells;

namespace Character.Combat
{
    public class MagicAttackStrategy : AttackStrategy
    {
        private SpellBook _spellBook;

        private ISpell _currentSpell;

        private ISpell _selectedSpell;

        protected override void InitInternal(CharacterModel model)
        {
             SetupSpellBook(model.SpellBook);
        }

        private void SetupSpellBook(SpellBook spellBook)
        {
            _spellBook = spellBook;
            _currentSpell = _spellBook.SelectedSpell;
            
            spellBook.OnSpellSelected += SpellSelected;

            void SpellSelected(ISpell spell)
            {
                _selectedSpell = spell;
                if(!AttackInProgress)
                {
                    UpdateCurrentSpell();
                }
            }
        }

        private void UpdateCurrentSpell()
        {
            if (_selectedSpell == null)
                return;
            
            _currentSpell = _selectedSpell;
            _selectedSpell = null;
        }
        
        protected override void AttackFinish()
        {
            base.AttackFinish();
            _currentSpell.EndCast(character.Model);
            
            UpdateCurrentSpell();
        }

        protected override void AttackStart()
        {
            base.AttackStart();
            _currentSpell.BeginCast(character.Model);
        }

        protected override bool CanAttack()
        {
            return base.CanAttack() && _currentSpell != null;
        }
    }
}