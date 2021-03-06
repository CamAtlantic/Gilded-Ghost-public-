using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public bool slow = false;
        public float normalSensitivity = 2f;
        public float slowSensitivity = 0.5f;

        private float XSensitivity = 2f;
        private float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;

        private float firePlayerRotateSpeed;
        private float fireCameraRotateSpeed;
        private bool firePlayerRotate = false;
        private bool fireCameraRotate = false;

        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;


        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }

        public void TriggerFirePlayerRotate(float speed)
        {
            firePlayerRotateSpeed = speed;
            firePlayerRotate = true;
        }

        public void TriggerFireCameraRotate(float speed)
        {
            fireCameraRotateSpeed = speed;
            fireCameraRotate = true;
        }

        public void EyelidSlow(bool onOff, float modifier)
        {
            slow = onOff;
            slowSensitivity = modifier;
        }

        public void LookRotation(Transform character, Transform camera)
        {
            if(slow)
            {
                XSensitivity = slowSensitivity;
                YSensitivity = slowSensitivity;
            }
            else
            {
                XSensitivity = normalSensitivity;
                YSensitivity = normalSensitivity;
            }

            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            //------this is where I'm adding the fire rotation;
            if (firePlayerRotate)
            {
                firePlayerRotate = false;
                m_CharacterTargetRot *= Quaternion.Euler(0, firePlayerRotateSpeed, 0);
            }
            if (fireCameraRotate)
            {
                fireCameraRotate = false;
                m_CameraTargetRot *= Quaternion.Euler(fireCameraRotateSpeed, 0, 0);
            }
            //------------------------------------------------------------

            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }
        }


        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
