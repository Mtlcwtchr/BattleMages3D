using Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterHud : MonoBehaviour
    {
        [SerializeField] private Slider hpBar;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text armor;

        private CharacterModel _character;
        
        public void Init(CharacterModel character)
        {
            _character = character;
            icon.sprite = character.Data.Config.icon;
            armor.text = character.Data.Armor.ToString(".#");
            
            CharacterHpChanged(character.Data.Hp);
            
            character.OnHpChanged += CharacterHpChanged;
        }

        private string GetHpText(CharacterData entity) => $"{entity.Hp}/{entity.MaxHp}";

        private void CharacterHpChanged(float hp)
        {
            hpBar.value = hp / _character.Data.MaxHp;
            hpText.text = GetHpText(_character.Data);
        }
    }
}