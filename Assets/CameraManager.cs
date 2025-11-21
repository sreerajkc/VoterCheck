using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineFreeLook thirdPersonCamera;

    private float thirdPersonCameraXSensitivity;
    private float thirdPersonCameraYSensitivity;

    private void Start()
    {
        thirdPersonCameraXSensitivity = thirdPersonCamera.m_XAxis.m_MaxSpeed;
        thirdPersonCameraYSensitivity = thirdPersonCamera.m_YAxis.m_MaxSpeed;
    }

    private void OnEnable()
    {
        Toilet.OnVoterEntered += DisableThirdPersonCamera;
        Toilet.OnVoterExit += EnableThirdPersonCamera;
    }


    private void OnDisable()
    {
        Toilet.OnVoterEntered -= DisableThirdPersonCamera;
        Toilet.OnVoterExit -= EnableThirdPersonCamera;
    }

    private void EnableThirdPersonCamera(VoterInteractionController controller)
    {
        thirdPersonCamera.enabled = true;
        WaitThirdPersonCameraToBlend();
    }


    private void DisableThirdPersonCamera(VoterInteractionController controller)
    {
        SetThirdPersonCameraSpeed(0, 0);
        thirdPersonCamera.enabled = false;
    }


    private async void WaitThirdPersonCameraToBlend()
    {
        await Task.Delay(1000);
        SetThirdPersonCameraSpeed(thirdPersonCameraXSensitivity, thirdPersonCameraYSensitivity);
    }

    private void SetThirdPersonCameraSpeed(float XAxisMaxSpeed, float YAxisMaxSpeed)
    {
        thirdPersonCamera.m_XAxis.m_MaxSpeed = XAxisMaxSpeed;
        thirdPersonCamera.m_YAxis.m_MaxSpeed = YAxisMaxSpeed;
    }
}
