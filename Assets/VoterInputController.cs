using UnityEngine;

public class VoterInputController : MonoBehaviour, IInputActivater
{
    private Camera mainCamera;

    private VoterMovementController voterMovementController;

    public bool IsInputEnabled { get; set; } = true;


    // Start is called before the first frame update
    void Start()
    {
        voterMovementController = GetComponent<VoterMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInputEnabled)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            voterMovementController.Move(new Vector2(horizontal, vertical));
        }
    }

    public void DisableInput()
    {
        IsInputEnabled = false;
    }

    public void EnableInput()
    {
        IsInputEnabled = true;
    }
}
