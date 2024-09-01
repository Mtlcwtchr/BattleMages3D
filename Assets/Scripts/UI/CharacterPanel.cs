using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterPanel : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text armorText;

        private CharacterModel _character;
        
        private void OnDestroy()
        {
            Clear();
        }
        
        public void SetContent(CharacterModel character)
        {
            Clear();
            
            _character = character;
            armorText.text = character.Data.Armor.ToString(".#");

            CharacterHpChanged(character.Data.Hp);
            
            _character.OnHpChanged += CharacterHpChanged;
        }

        private void Clear()
        {
            if (_character != null) 
                _character.OnHpChanged -= CharacterHpChanged;

            _character = null;
        }

        private string GetHpText(CharacterData entity) => $"{entity.Hp}/{entity.MaxHp}";

        private void CharacterHpChanged(float hp)
        {
            hpBar.value = hp / _character.Data.MaxHp;
            hpText.text = GetHpText(_character.Data);
        }
    }
}