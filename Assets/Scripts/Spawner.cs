using Fusion.Sockets;
using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Schema;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkObject voterPrefab;
    [SerializeField] private NetworkObject officerPrefab;
    
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    private VoterInputController voterInputController;
    private VoterInteractionController voterInteractionController;

    private int officerSpawnIndex = -1;
    private int currentSpawnIndex;

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("Connected To Server");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log("Player Joined");

        int totalPlayerCount = runner.CommittedPlayers.Count();

        if (runner.IsServer)
        {
            if (officerSpawnIndex < 0)
            {
                officerSpawnIndex = UnityEngine.Random.Range(0, totalPlayerCount);
            }

            NetworkObject networkPlayerObject;
            if (currentSpawnIndex == officerSpawnIndex)
            {
                Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(-40f, 40f), 0, UnityEngine.Random.Range(-40f, 40f));
                networkPlayerObject = runner.Spawn(officerPrefab, spawnPosition, Quaternion.identity, player);
            }
            else
            {
                Vector3 spawnPosition = new Vector3(10 , 0, 15);
                networkPlayerObject = runner.Spawn(voterPrefab, spawnPosition, Quaternion.identity, player);
            }

            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }


    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (NetworkPlayer.Local != null)
        {
            if (voterInputController == null)
            {
                voterInputController = NetworkPlayer.Local.GetComponent<VoterInputController>();
            }

            if (voterInteractionController == null)
            {
                voterInteractionController = NetworkPlayer.Local.GetComponent<VoterInteractionController>();
            }
        }

        if (voterInputController != null)
        {
            input.Set(voterInputController.GetNetworkedInputData());
        }
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

}
