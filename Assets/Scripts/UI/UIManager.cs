using UnityEngine;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SpellsPanel spellsPanel;
        [SerializeField] private CharacterPanel characterPanel;
        [SerializeField] private GameObject endgameElement;

        private CharacterController _character;

        public void SetContent(CharacterController character)
        {
            _character = character;

            spellsPanel.SetData(character.Entity.SpellBook);
            characterPanel.SetContent(character);
            
            _character.OnDead += CharacterDead;
        }

        private void CharacterDead()
        {
            endgameElement.SetActive(true);
            spellsPanel.gameObject.SetActive(false);
            characterPanel.gameObject.SetActive(false);
        }
    }
}