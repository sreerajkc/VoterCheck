using Fusion;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VoterInputController : NetworkBehaviour, IInputActivater
{
    [SerializeField] private NetworkPlayer networkPlayer;
    [SerializeField] private Transform cameraLookAtPoint;

    [SerializeField] private VoterMovementController voterMovementController;
    [SerializeField] private VoterInteractionController voterInteractionController;
    private Camera mainCamera;

    private NetworkInputData networkInputData = new NetworkInputData();
    private NetworkInputData localInputData = new NetworkInputData();

    [Networked] public bool IsInputEnabled { get; set; } = true;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        networkPlayer.onLocalPlayerSpawned += SetCamera;
    }

    private void OnDisable()
    {
        networkPlayer.onLocalPlayerSpawned -= SetCamera;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public override void Spawned()
    {
        base.Spawned();
    }
    public override void FixedUpdateNetwork()
    {
        if (!Object.HasInputAuthority && !Object.HasStateAuthority)
            return;

        if (GetInput(out NetworkInputData networkInput))
        {
            voterMovementController.Move(networkInput);

            if (Object.HasStateAuthority)
            {
                if (networkInput.InteractButtonDown)
                {
                    voterInteractionController.EnterCurrentEnterable();
                }

                if (networkInput.CancelButtonDown)
                {
                    voterInteractionController.ExitCurrentExitable();
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!networkPlayer.IsLocal)
            return;

        if (IsInputEnabled)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            localInputData.Direction = new Vector2(horizontal, vertical);

            if (Input.GetButtonDown("Jump"))
            {
                localInputData.JumpButtonDown = true;
            }

            if (Input.GetButtonDown("Interact"))
            {
                localInputData.InteractButtonDown = true;
            }

            localInputData.CameraEulerY = mainCamera.transform.eulerAngles.y;
        }
        else
        {
            if (Input.GetButtonDown("Cancel"))
            {
                localInputData.CancelButtonDown = true;
            }

            localInputData.Direction = Vector2.zero;
            localInputData.CameraEulerY = 0;
        }
    }

    public NetworkInputData GetNetworkedInputData()
    {
        networkInputData = localInputData;

        localInputData.JumpButtonDown = false;
        localInputData.InteractButtonDown = false;
        localInputData.CancelButtonDown = false;

        return networkInputData;
    }


    public void DisableInput()
    {
        //Cursor.visible = true;
        IsInputEnabled = false;
    }

    public void EnableInput()
    {
        //Cursor.visible = false;
        IsInputEnabled = true;
    }

    private void SetCamera()
    {
        CameraManager.Instance.InitializeThirdPersonCamera(cameraLookAtPoint);
    }
}
