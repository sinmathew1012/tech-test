using System;
using TechnicalTest.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TechnicalTest
{
    public class UIPanel : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager;
        public static UIPanel Instance { get; private set; }
        public event Action<int, int> OnMaterialOptionClicked;
        public event Action<int, int> OnMeshOptionClicked;

        [SerializeField] private GameObject GO_IdlePanel;
        [SerializeField] private GameObject GO_ModificationPanel;

        [SerializeField] private Image[] Img_MaterialOptions = new Image[4];
        [SerializeField] private Image[] Img_MeshOptions = new Image[4];

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            PlayerStateManager.OnPlayerStateChanged += HandlePlayerStateChanged;
            ShowIdle();
        }
        private void OnDestroy()
        {
            PlayerStateManager.OnPlayerStateChanged -= HandlePlayerStateChanged;
        }

        private void HandlePlayerStateChanged(PlayerState newState)
        {
            if (newState == PlayerState.Idle)
            {
                ShowIdle();
            }
            else if (newState == PlayerState.Modifying)
            {
                ShowModifyPanel();
            }
        }

        public void ShowIdle()
        {
            GO_IdlePanel.gameObject.SetActive(true);
            GO_ModificationPanel.gameObject.SetActive(false);
        }
    
        public void ShowModifyPanel()
        {
            /*
             * take data from selected Sphere
             * set button images to reference from Sphere's MaterialLibrary and MeshIconLibrary
             */
            var currentSphereData = SphereDataManager.Instance.SphereDataArray[PlayerStateManager.CurrentSelectedSphereId];
            for (int i = 0; i < Img_MaterialOptions.Length; i++)
            {
                Img_MaterialOptions[i].material = currentSphereData.MaterialLibrary[i];
            }
            for (int i = 0; i < Img_MeshOptions.Length; i++)
            {
                Img_MeshOptions[i].sprite = currentSphereData.MeshIconLibrary[i];
            }
        
            //show panel after all image set
            GO_IdlePanel.gameObject.SetActive(false);
            GO_ModificationPanel.gameObject.SetActive(true);
        }
    
        public void OnClickMaterialOption(int optionId)
        {
            var currentSelectedSphereId = PlayerStateManager.CurrentSelectedSphereId;
            OnMaterialOptionClicked?.Invoke(currentSelectedSphereId, optionId);
        }
        public void OnClickMeshOption(int optionId)
        {
            var currentSelectedSphereId = PlayerStateManager.CurrentSelectedSphereId;
            OnMeshOptionClicked?.Invoke(currentSelectedSphereId, optionId);
        }

        public void OnClickGoBack()
        {
            PlayerStateManager.ChangeState(PlayerState.Idle);
        }
    
        public void OnClickSwitchLevel()
        {
            levelManager.SwitchLevel();
        }
    }
}
