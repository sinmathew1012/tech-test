using System;
using UnityEngine;

namespace Script
{
    public class PlayerMovementController : MonoBehaviour
    {
        public static PlayerMovementController Instance;
        // public Transform IdlePosition;
        public Transform[][] modificationPoints;
        public GameObject[] LevelViewPointGroup;
        private float movementSpeed = 10f;
        private float rotationSpeed = 15f;

        private int currentPointIndex = 0;
        private bool isMoving = false;

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
            modificationPoints = new Transform[LevelManager.Instance.levelNames.Length][];
            for (int i = 0; i < LevelViewPointGroup.Length; i++)
            {
                Transform[] viewPointsPerLevel = LevelViewPointGroup[i].GetComponentsInChildren<Transform>();
                modificationPoints[i] = new Transform[viewPointsPerLevel.Length];
                for (int j = 1; j < viewPointsPerLevel.Length; j++)
                {
                    modificationPoints[i][j-1] = viewPointsPerLevel[j];
                }
            }
            
            transform.position = modificationPoints[0][0].position;
            transform.rotation = modificationPoints[0][0].rotation;
        }

        private void Update()
        {
            if (isMoving)
            {
                MoveCamera();
            }
        }
        public void TriggerMoveCamera(int toPointIndex)
        {
            currentPointIndex = toPointIndex;
            isMoving = true;
        }

        public void MoveCamera()
        {
            var levelIndex = LevelManager.Instance.CurrentLevelIndex;
            Vector3 targetPosition = modificationPoints[levelIndex][currentPointIndex].position;
            Quaternion targetRotation = modificationPoints[levelIndex][currentPointIndex].rotation;

            transform.position =
                Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (transform.position == targetPosition && transform.rotation == targetRotation)
            {
                isMoving = false;
            }
        }
    }
}