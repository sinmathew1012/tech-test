using UnityEngine;

namespace Script
{
    public class IdlePanel: MonoBehaviour
    {
        public static IdlePanel Instance { get; private set; }
        [SerializeField] private GameObject GO_IdlePanel;
        // [SerializeField] private TMP_Text Text_NextLevel;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        private void Start()
        {
            UIStateManger.Instance.OnUIStateChanged += HandleUIStateChanged;
        }
        private void OnDestroy()
        {
            UIStateManger.Instance.OnUIStateChanged -= HandleUIStateChanged;
        }

        private void HandleUIStateChanged(UIState newState)
        {
            if (newState == UIState.Idle)
            {
                GO_IdlePanel.gameObject.SetActive(true);
                // Text_NextLevel.text = "To " + LevelSwitcher.Instance.GetNextLevelName();
            }
            else if (newState == UIState.Modifying)
            {
                GO_IdlePanel.gameObject.SetActive(false);

            }
        }
        
        public void OnClickSwitchLevel()
        {
            LevelManager.Instance.SwitchLevel();
            // Text_NextLevel.text = "To " + LevelSwitcher.Instance.GetNextLevelName();
            // UIStateManger.Instance.ChangeState(UIState.Idle);
        }
    }
}