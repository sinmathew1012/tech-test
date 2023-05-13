using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TechnicalTest.Manager
{
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// all level tobe loaded, not include TestScene cause it always been loaded in background
        /// </summary>
        public static readonly string[] LevelNames = new string[]{"Level_1", "Level_2"};

        /// <summary>
        /// pointers to the current level
        /// </summary>
        public static int CurrentLevelIndex = 0;
        public static string CurrentLevelName = "Level_1";
        private bool isLoading = false;

        private void Start()
        {
            SceneManager.LoadSceneAsync(LevelNames[0]); // load level_1 in the beginning 
        }
        
        public void SwitchLevel()
        {
            if (!isLoading)
            {
                StartCoroutine(CoLoadLevel());
            }
        }
        
        /// <summary>
        /// coroutine load level and turn ON level scene when its done
        /// </summary>
        /// <returns></returns>
        private IEnumerator CoLoadLevel()
        {
            // loadingIcon.SetActive(true);


            int nextLevelIndex = GetNextLevelIndex();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LevelNames[nextLevelIndex]);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }

                yield return null;
            }
            
            /*
             * GC after scene load to redcue memory usage
             */
            Resources.UnloadUnusedAssets();
            
            // loadingIcon.SetActive(false);
            CurrentLevelIndex = nextLevelIndex;
            CurrentLevelName = LevelNames[nextLevelIndex];
            isLoading = false;
        }

        /// <summary>
        /// loop between levelNames array
        /// </summary>
        /// <returns></returns>
        private int GetNextLevelIndex()
        {
            int nextLevelIndex = CurrentLevelIndex + 1;
            if (nextLevelIndex >= LevelNames.Length)
            {
                nextLevelIndex = 0;
            }

            return nextLevelIndex;
        }
    }
}