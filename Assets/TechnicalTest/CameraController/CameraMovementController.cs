using System.Collections.Generic;
using TechnicalTest.Manager;
using UnityEngine;

namespace TechnicalTest.CameraController
{
    public class CameraMovementController : MonoBehaviour
    {
        [SerializeField] private GameObject[] levelViewPointGroup;

        private Dictionary<string, Transform[]> levelViewPortDict = new Dictionary<string, Transform[]>();
        
        private readonly float movementSpeed = 10f; // camera movement parameter
        private readonly float rotationSpeed = 15f; // camera movement parameter

        private int targetViewPortIndex = 0; // camera chase for the target viewPort every frame
        private bool isMoving = false;

        private void Start()
        {
            /*
             * store all viewports from level_1 and level_2 in a dictionary(levelViewPortDict)
             * levelViewPortDict store dedicated transform for camera to move to, when player selected a sphere
             */
            for (int i = 0; i < LevelManager.LevelNames.Length; i++)
            {
                levelViewPortDict.Add(LevelManager.LevelNames[i], GetViewPointsFromGameObject(levelViewPointGroup[i]));
            }

            /*
             * camera initial port
             */
            transform.position = levelViewPortDict[LevelManager.CurrentLevelName][0].position;
            transform.rotation = levelViewPortDict[LevelManager.CurrentLevelName][0].rotation;

            PlayerStateManager.OnPlayerStateChanged += HandlePlayerStateChange;
        }

        private void Update()
        {
            if (isMoving)
            {
                MoveCamera();
            }
        }

        private void OnDestroy()
        {
            PlayerStateManager.OnPlayerStateChanged -= HandlePlayerStateChange;
        }

        private void HandlePlayerStateChange(PlayerState newState)
        {
            if (newState == PlayerState.Idle)
            {
                TriggerMoveCamera(0);
            }
            else if (newState == PlayerState.Modifying)
            {
                TriggerMoveCamera(PlayerStateManager.CurrentSelectedSphereId + 1);
            }
        }
        
        /// <summary>
        /// set viewport transform as camera target
        /// </summary>
        /// <param name="viewPortIndex"></param>
        private void TriggerMoveCamera(int viewPortIndex)
        {
            this.targetViewPortIndex = viewPortIndex;
            isMoving = true;
        }

        /// <summary>
        /// Camera chase to target per frame
        /// </summary>
        private void MoveCamera()
        {
            Transform targetTransform = levelViewPortDict[LevelManager.CurrentLevelName][targetViewPortIndex];
            Vector3 targetPosition = targetTransform.position;
            Quaternion targetRotation = targetTransform.rotation;

            transform.position =
                Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (transform.position == targetPosition && transform.rotation == targetRotation)
            {
                isMoving = false;
            }
        }
        
        /// <summary>
        /// find all transform under the parent, return them in an array (except the parent) 
        /// </summary>
        /// <param name="viewPointsParent"> gameobject which include all the viewports for a single level</param>
        /// <returns></returns>
        private Transform[] GetViewPointsFromGameObject(GameObject viewPointsParent)
        {
            Transform[] transformsInChild = viewPointsParent.GetComponentsInChildren<Transform>();
            Transform[] viewPorts = new Transform[transformsInChild.Length];
            for (int i = 1; i < transformsInChild.Length; i++)
            {
                viewPorts[i-1] = transformsInChild[i];
            }
            return viewPorts;
        }
    }
}