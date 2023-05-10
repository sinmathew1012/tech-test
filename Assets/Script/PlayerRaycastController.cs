using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering;

namespace Script
{
    public class PlayerRaycastController: MonoBehaviour
    {
        private bool touchInProgress = false;
        private float touchTimeThreshold = 0.2f;
        private float touchStartTime;
        private Camera mainCamera;
        private Vector3 screenCenter;
        private void Awake()
        {
            mainCamera = Camera.main;
            screenCenter = new Vector3(Screen.width * .5f, Screen.height * .5f, 0f);
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    touchInProgress = true;
                    touchStartTime = Time.time;
                }
                else if (touch.phase == TouchPhase.Ended && touchInProgress)
                {
                    touchInProgress = false;
                    ProcessTouch(touch.position);
                }
            }
            else if (Input.GetMouseButtonDown(0))
            {
                ProcessTouch(Input.mousePosition);
            }
        }

        private void ProcessTouch(Vector3 touchPosition)
        {
            // if (Time.time - touchStartTime <= touchTimeThreshold)
            // {
                Ray ray = mainCamera.ScreenPointToRay(touchPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name != "Plane")
                    {
                        Destroy(hit.transform.gameObject);
                    }
                    // Debug.Log(hit.transform.gameObject.name);
                }
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1f);
            // }
        }
    }
}