using Character.Combat.Spells.Config;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SpellElement : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private GameObject selected;

        public void SetContent(SpellConfig config)
        {
            icon.sprite = config.icon;
        }

        public void SetSelected(bool isSelected)
        {
            selected.SetActive(isSelected);
        }
    }
}