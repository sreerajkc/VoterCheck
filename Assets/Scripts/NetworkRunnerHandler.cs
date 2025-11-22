using Fusion;
using Fusion.Sockets;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;

    private NetworkRunner networkRunner;

    // Start is called before the first frame update
    async void Start()
    {
        networkRunner = Instantiate(networkRunnerPrefab);
        networkRunner.name = "NetworkRunner";
        var clientTask =  InitializeNetworkRunner(networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(), SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), null);
        await clientTask;
        Debug.Log("Networkrunner Started");
    }
    
    protected virtual Task InitializeNetworkRunner
        (NetworkRunner runner,GameMode gameMode,NetAddress netAddress,SceneRef scene,Action<NetworkRunner> initialized)
    {

        var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager == null)
        {
            sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        runner.ProvideInput = true;

        return runner.StartGame(new StartGameArgs
        { 
            GameMode = gameMode,
            Address = netAddress,
            Scene = scene,
            SessionName = "TestRoom",
            OnGameStarted = initialized,
            SceneManager = sceneManager
        }
        );


    }
}
