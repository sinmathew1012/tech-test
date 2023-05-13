using UnityEngine;

namespace TechnicalTest.Manager
{
    /// <summary>
    /// a place to store all the shared SphereData settings
    /// Data won't get lost across level switches
    /// </summary>
    public class SphereDataManager : MonoBehaviour
    {
        public static SphereDataManager Instance { get; private set; }
        public SphereData[] SphereDataArray {get; private set;}

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

            SphereDataArray = new SphereData[4];
            for (int i = 0; i < 4; i++)
            {
                SphereDataArray[i] = new SphereData(){HasBuilt = false};
            }
        }
    }
}