using System;
using Camera;
using Character;
using UI;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace SceneManagement
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private FollowUpCamera followUpCamera;
        [SerializeField] private CharacterController character;
        [SerializeField] private CharacterConfig playerConfig;
        [SerializeField] private EnemySpawnController enemiesController;
        [SerializeField] private UIManager uiManager;
        
        private void OnEnable()
        {
            var playerModel = new CharacterModel(playerConfig);
            
            character.Init(playerModel);
            followUpCamera.SetFollowingObject(playerModel);
            enemiesController.SetTarget(playerModel);
            uiManager.SetContent(playerModel);
            
            playerModel.OnCharacterDied += CharacterDied;
        }

        private void CharacterDied()
        {
            Time.timeScale = 0.01f;
        }
    }
}