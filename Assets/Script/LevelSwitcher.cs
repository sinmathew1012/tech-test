using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class LevelSwitcher : MonoBehaviour
    {
        public string[] levelNames;
        private int currentLevelIndex = -1;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (currentLevelIndex < 0)
            {
                SwitchLevel();
            }
        }

        public void LoadScene(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }
        public void SwitchLevel()
        {
            currentLevelIndex = (currentLevelIndex + 1) % levelNames.Length;
            LoadScene(levelNames[currentLevelIndex]);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                SwitchLevel();
            }
        }
    }
}