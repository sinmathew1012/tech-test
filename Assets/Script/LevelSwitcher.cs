using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script
{
    public class LevelSwitcher : MonoBehaviour
    {
        public static LevelSwitcher Instance;
        public string[] levelNames;
        private int currentLevelIndex = -1;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
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

        public string GetNextLevelName()
        {
            int nextLevel = (currentLevelIndex + 1) % levelNames.Length;
            return levelNames[nextLevel];
        }
        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.L))
        //     {
        //         SwitchLevel();
        //     }
        // }
    }
}