using Fusion;
using UnityEngine;

public class VoterMovementController : NetworkBehaviour
{
    private NetworkCharacterController controller;
    private NetworkPlayer networkPlayer;
    private Camera mainCamera;

    void Awake()
    {
        controller = GetComponent<NetworkCharacterController>();
        mainCamera = Camera.main;
        networkPlayer = GetComponent<NetworkPlayer>();
    }

    public void Move(NetworkInputData networkInputData)
    {
        Vector3 direction = new Vector3(networkInputData.Direction.x, 0, networkInputData.Direction.y);
        Vector3 moveDirection = Vector3.zero;


        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + networkInputData.CameraEulerY;
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        if (networkInputData.JumpButtonDown)
        {
            controller.Jump();
        }
        Debug.Log((networkPlayer.IsLocal ? "[Local]" : "[Remote]:") + moveDirection, gameObject);

        controller.Move(moveDirection * Runner.DeltaTime);
    }
}


