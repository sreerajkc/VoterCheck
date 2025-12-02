using Fusion;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkRunnerHandler : MonoBehaviour
{
    [SerializeField] private NetworkRunner networkRunnerPrefab;

    public NetworkRunner Runner { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public async Task CreateRoom()
    {
        await StartRunner(GameMode.Host, RoomCodeGenerator.Generate(5));
    }

    public async Task JoinRoom(string roomCode)
    {
        roomCode = roomCode.ToUpper();
        await StartRunner(GameMode.Client, roomCode);
    }

    private async Task StartRunner(GameMode gameMode, string roomCode)
    {
        Runner = Instantiate(networkRunnerPrefab);
        Runner.name = "NetworkRunner";

        var sceneManager = Runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>().FirstOrDefault();

        if (sceneManager == null)
        {
            sceneManager = Runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        Runner.ProvideInput = true;
        var sceneRef = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);

        var result = await Runner.StartGame(new StartGameArgs
        {
            GameMode = gameMode,
            Scene = sceneRef,
            SessionName = roomCode,
            SceneManager = sceneManager
        }
        );

        Debug.Log(roomCode);
    }
}
