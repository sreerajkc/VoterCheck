using UnityEngine;

public class VoterMovementController : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private Camera mainCamera;
    private float turnSmoothVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    public void Move(Vector2 input)
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 direction = new Vector3(input.x, 0, input.y);
        Vector3 moveDirection = Vector3.zero;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        // Jump
        if (Input.GetButton("Jump") && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        // Apply gravity
        playerVelocity.y += gravity * Time.deltaTime;

        // Combine horizontal and vertical movement
        Vector3 finalMove = moveDirection * speed + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
}
