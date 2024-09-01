using Character;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SpellsPanel spellsPanel;
        [SerializeField] private CharacterPanel characterPanel;
        [SerializeField] private GameObject endgameElement;

        private CharacterModel _character;

        public void SetContent(CharacterModel character)
        {
            _character = character;

            spellsPanel.SetData(character.SpellBook);
            characterPanel.SetContent(character);
            
            _character.OnCharacterDied += CharacterDead;
        }

        private void CharacterDead()
        {
            endgameElement.SetActive(true);
            spellsPanel.gameObject.SetActive(false);
            characterPanel.gameObject.SetActive(false);
        }
    }
}