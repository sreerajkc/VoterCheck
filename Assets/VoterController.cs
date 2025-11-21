using UnityEngine;

public class VoterController : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movement Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime;

    private Camera mainCamera;
    private float turnSmoothVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3 (horizontal, 0, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)  * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);

            Vector3 moveDirection   = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }
}
