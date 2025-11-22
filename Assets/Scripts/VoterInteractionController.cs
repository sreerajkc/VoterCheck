using DG.Tweening;
using UnityEngine;

public class VoterInteractionController : MonoBehaviour
{
    private IEnterable currentEnterable;
    private IExitable currentExitable;

    private IInputActivater inputActivater;

    private void Start()
    {
        inputActivater = GetComponent<IInputActivater>();
    }

    private void Update()
    {
        if (currentEnterable != null && Input.GetButtonDown("Interact"))
        {
            currentEnterable.Enter(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IEnterable>(out IEnterable enterable))
        {
            currentEnterable =  enterable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentEnterable != null && other.TryGetComponent<IEnterable>(out IEnterable enterable))
        {
            currentEnterable = enterable;

            if (enterable is IExitable)
            {
                currentExitable = enterable as IExitable;
            }
        }
    }

    public void OnEnterToilet()
    {
        inputActivater.DisableInput();
        transform.forward = Vector3.forward;
    }

    public void OnExitToilet()
    {
        DOVirtual.DelayedCall(1, inputActivater.EnableInput);
    }
}
