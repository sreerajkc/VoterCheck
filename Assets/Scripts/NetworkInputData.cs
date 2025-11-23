using Fusion;
using UnityEngine;

public struct NetworkInputData :INetworkInput
{
    public Vector2 Direction;
    public bool JumpButtonDown;
    public float CameraEulerY;
    public bool InteractButtonDown;
    public bool CancelButtonDown;
}
