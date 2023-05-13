using System;
using TechnicalTest.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TechnicalTest.CameraController
{
    public class CameraRaycastController: MonoBehaviour
    {
        private bool touchInProgress = false;
        private Camera mainCamera;
        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject())
            {
                /*
                 * mobile touch detection
                 */
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    touchInProgress = true;
                }
                else if (touch.phase == TouchPhase.Ended && touchInProgress)
                {
                    touchInProgress = false;
                    ProcessTouch(touch.position);
                }
            }
            else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                /*
                 * desktop touch detection
                 */
                ProcessTouch(Input.mousePosition);
            }
        }

        /// <summary>
        /// When player raycast hit on a collider, trigger ProcessTouch().
        /// When the hit is a Sphere(selectable object), reassign target and change state to Modify  
        /// </summary>
        /// <param name="touchPosition"></param>
        private void ProcessTouch(Vector3 touchPosition)
        {
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Sphere"))
                {
                    Sphere sphereTarget = hit.transform.GetComponent<Sphere>();
                    PlayerStateManager.CurrentSelectedSphereId = sphereTarget.sphereId; 
                    PlayerStateManager.ChangeState(PlayerState.Modifying);
                }
            }
#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
#endif
        }
    }
}