using Character.Entity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Character.CharacterController;

namespace UI
{
    public class CharacterPanel : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text armorText;

        private CharacterController _character;
        
        private void OnDestroy()
        {
            Clear();
        }
        
        public void SetContent(CharacterController character)
        {
            Clear();
            _character = character;
            armorText.text = character.Entity.Armor.ToString(".#");

            CharacterHpChanged(character.Entity.Hp);
            _character.OnHpChanged += CharacterHpChanged;
        }

        private void Clear()
        {
            if (_character != null)
            {
                _character.OnHpChanged -= CharacterHpChanged;
            }

            _character = null;
        }

        private string GetHpText(Entity entity) => $"{entity.Hp}/{entity.MaxHp}";

        private void CharacterHpChanged(float hp)
        {
            hpBar.value = hp / _character.Entity.MaxHp;
            hpText.text = GetHpText(_character.Entity);
        }
    }
}