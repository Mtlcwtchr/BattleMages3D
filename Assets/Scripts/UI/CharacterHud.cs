using Character.Entity;
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

        private Character.CharacterController _character;
        
        public void Init(Character.CharacterController character)
        {
            _character = character;

            icon.sprite = character.Entity.Config.icon;
            armor.text = character.Entity.Armor.ToString(".#");
            CharacterHpChanged(character.Entity.Hp);
            character.OnHpChanged += CharacterHpChanged;
            
        }

        private string GetHpText(Entity entity) => $"{entity.Hp}/{entity.MaxHp}";

        private void CharacterHpChanged(float hp)
        {
            hpBar.value = hp / _character.Entity.MaxHp;
            hpText.text = GetHpText(_character.Entity);
        }
    }
}