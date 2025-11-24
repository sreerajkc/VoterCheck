using Cinemachine;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineFreeLook thirdPersonCamera;

    private float thirdPersonCameraXSensitivity;
    private float thirdPersonCameraYSensitivity;

    private void Awake()
    {
        Instance = this;
    }

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

    public void InitializeThirdPersonCamera(Transform player)
    {
        thirdPersonCamera.Follow = player;
        thirdPersonCamera.LookAt = player;
    }

    public void EnableThirdPersonCamera()
    {
        thirdPersonCamera.enabled = true;
        WaitThirdPersonCameraToBlend();
    }


    public void DisableThirdPersonCamera()
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

    public void ChangeCameraPosition(Transform cameraPoint)
    {
        thirdPersonCamera.transform.SetPositionAndRotation(cameraPoint.position, cameraPoint.rotation);
    }

    public void ChangeCameraTarget(Transform cameraPointLookAt)
    {
        thirdPersonCamera.Follow = cameraPointLookAt;
        thirdPersonCamera.LookAt = cameraPointLookAt;
    }
}
