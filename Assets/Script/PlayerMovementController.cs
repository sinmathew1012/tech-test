using System;
using UnityEngine;

namespace Script
{
    public class PlayerMovementController : MonoBehaviour
    {
        public static PlayerMovementController Instance;
        public Transform[] modificationPoints;
        private float movementSpeed = 5f;
        private float rotationSpeed = 8f;

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
            transform.position = modificationPoints[currentPointIndex].position;
            transform.rotation = modificationPoints[currentPointIndex].rotation;
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
            // if (!isMoving)
            // {
                // currentPointIndex++;
                currentPointIndex = toPointIndex;
                // if (currentPointIndex >= modificationPoints.Length)
                // {
                //     currentPointIndex = 0;
                // }

                isMoving = true;
            // }
        }

        public void MoveCamera()
        {
            Vector3 targetPosition = modificationPoints[currentPointIndex].position;
            Quaternion targetRotation = modificationPoints[currentPointIndex].rotation;

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