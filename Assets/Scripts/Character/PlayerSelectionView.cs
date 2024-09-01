using UnityEngine;

namespace Character
{
    public class PlayerSelectionView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera followUpCamera;
        [SerializeField] private PlayerCharacterController player; 
        [SerializeField] private GameObject selectionMark;

        private bool _isVisible;

        private bool Visible
        {
            get => _isVisible;
            set
            {
                if (_isVisible == value)
                    return;

                _isVisible = value;
                selectionMark.SetActive(_isVisible);
            }
        }

        private void Awake()
        {
            _isVisible = selectionMark.activeSelf;
        }

        private void FixedUpdate()
        {
            Visible = player.SelectionVisible;
            if (!Visible)
                return;
            
            UpdatePosition(player.SelectionPosition, player.SelectionNormal);
        }

        private void UpdatePosition(Vector3 pos, Vector3 normal)
        {
            selectionMark.transform.SetPositionAndRotation(pos, Quaternion.LookRotation(normal));
        }
    }
}