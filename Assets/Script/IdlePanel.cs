using System;
using UnityEngine;

namespace Script
{
    public class IdlePanel: MonoBehaviour
    {
        public static IdlePanel Instance { get; private set; }
        [SerializeField] private GameObject GO_IdlePanel;
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
            UIStateManger.Instance.OnUIStateChanged += HandleUIStateChanged;
        }
        private void OnDestroy()
        {
            UIStateManger.Instance.OnUIStateChanged -= HandleUIStateChanged;
        }

        private void HandleUIStateChanged(UIState newState)
        {
            switch (newState)
            {
                case UIState.Idle:
                    GO_IdlePanel.gameObject.SetActive(true);
                    break;
                case UIState.Modifying:
                    GO_IdlePanel.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        
        public void OnClickSwitchLevel()
        {
            LevelManager.Instance.SwitchLevel();
        }
    }
}