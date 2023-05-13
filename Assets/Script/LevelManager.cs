using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Script
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        public string[] levelNames;
        public int CurrentLevelIndex = 0;
        private bool isLoading = false;

        private void Awake()
        {
            #region singleton logic

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
            #endregion
        }
        private void Start()
        {
            SceneManager.LoadSceneAsync(levelNames[0]);
        }
        public void SwitchLevel()
        {
            if (!isLoading)
            {
                StartCoroutine(LoadLevelAsync());
            }
        }
        
        private IEnumerator LoadLevelAsync()
        {
            // loadingIcon.SetActive(true);

            Resources.UnloadUnusedAssets();

            int nextLevelIndex = GetNextLevelIndex();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(levelNames[nextLevelIndex]);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }

            // loadingIcon.SetActive(false);
            CurrentLevelIndex = nextLevelIndex;
            isLoading = false;
        }

        private int GetNextLevelIndex()
        {
            int nextLevelIndex = CurrentLevelIndex + 1;
            if (nextLevelIndex >= levelNames.Length)
            {
                nextLevelIndex = 0;
            }

            return nextLevelIndex;
        }
    }
}