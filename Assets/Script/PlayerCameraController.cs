using System;
using UnityEngine;

namespace Script
{
    public class PlayerCameraController : MonoBehaviour
    {
        private float sensitivityHeight = 1f;
        private float sensitivityWidth = 1f;
        private float yaw = 0f;
        private float pitch = 0f;
        
        private void Start()
        {
            // Cursor.visible = false;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    yaw += sensitivityHeight * touch.deltaPosition.x;
                    pitch -= sensitivityWidth * touch.deltaPosition.y;
                    
                    transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
                }
            }
            else
            {
                yaw += sensitivityHeight * Input.GetAxis("Mouse X");
                pitch -= sensitivityWidth * Input.GetAxis("Mouse Y");
                transform.eulerAngles = new Vector3(pitch, yaw, 0f);
            }

        }
    }
}