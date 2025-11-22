using Fusion;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VoterInputController : NetworkBehaviour, IInputActivater
{
    [SerializeField] private NetworkPlayer networkPlayer; 
    [SerializeField] private Transform cameraLookAtPoint;

    private VoterMovementController voterMovementController;

    private Vector2 input;
    private bool jumpButtonDown;
    private float cameraEulerY;

    private Camera mainCamera;


    private NetworkInputData networkInputData = new NetworkInputData();

    public bool IsInputEnabled { get; set; } = true;

    private void OnEnable()
    {
        networkPlayer.onLocalPlayerSpawned += SetCamera;   
    }

    private void OnDisable()
    {
        networkPlayer.onLocalPlayerSpawned -= SetCamera;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;      
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

            input = new Vector2(horizontal, vertical);

            if (Input.GetButtonDown("Jump"))
                jumpButtonDown = true;
            cameraEulerY = mainCamera.transform.eulerAngles.y;
        }
        else
        {
            input = Vector2.zero;
            cameraEulerY = 0;
        }


    }

    public NetworkInputData GetNetworkedInputData()
    {
        networkInputData.Direction = input;
        networkInputData.JumpButtonDown = jumpButtonDown;
        networkInputData.CameraEulerY = cameraEulerY;

        jumpButtonDown = false;

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
