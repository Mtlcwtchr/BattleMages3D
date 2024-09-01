using Camera;
using UI;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace SceneManagement
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private FollowUpCamera followUpCamera;
        [SerializeField] private CharacterController character;
        [SerializeField] private EnemySpawnController enemiesController;
        
        [SerializeField] private UIManager uiManager;
        
        private void Awake()
        {
            character.OnDead += CharacterDead;
            character.Init();
            
            followUpCamera.SetFollowingObject(character);
            enemiesController.SetTarget(character);

            uiManager.SetContent(character);
        }

        private void CharacterDead()
        {
            Time.timeScale = 0.01f;
        }
    }
}