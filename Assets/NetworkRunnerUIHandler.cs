using Fusion;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkRunnerUIHandler : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private NetworkRunnerHandler networkRunnerHandler;

    [Header("UI Properties")]
    [SerializeField] private GameObject roomCreateJoinPanel;
    [SerializeField] private TMP_InputField playerNameText;
    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_InputField joinRoomCodeInputField;
    [Space(10)]
    [SerializeField] private GameObject roomPlayerPanel;
    [SerializeField] private PlayerNameUI playerNameUIPref;
    [SerializeField] private RectTransform playerNamesParentTransform;
    [SerializeField] private Button hostStartButton;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI connectionStatusText;

    private Dictionary<int, PlayerNameUI> connectedPlayerUIs = new Dictionary<int, PlayerNameUI>();

    private void Awake()
    {
        createButton.onClick.AddListener(OnClickCreateButton);
        joinButton.onClick.AddListener(OnClickJoinButton);
    }

    private void OnEnable()
    {
        NetworkRunnerCallbackHandler.OnPlayerJoinedServer += UpdatePlayerListOnJoin;
        NetworkRunnerCallbackHandler.OnPlayerLeftServer += UpdatePlayerListOnLeave;
    }

    private void OnDisable()
    {
        NetworkRunnerCallbackHandler.OnPlayerJoinedServer -= UpdatePlayerListOnJoin;
        NetworkRunnerCallbackHandler.OnPlayerLeftServer -= UpdatePlayerListOnLeave;
    }

    private async void OnClickCreateButton()
    {
        createButton.interactable = false;
        joinButton.interactable = false;

        connectionStatusText.text = "Creating Room...";

        await networkRunnerHandler.CreateRoom();

        connectionStatusText.text = "Created Room Successfully!" + networkRunnerHandler.Runner.SessionInfo.Name;

        roomCreateJoinPanel.SetActive(false);
        roomPlayerPanel.SetActive(true);
    }

    private async void OnClickJoinButton()
    {
        createButton.interactable = false;
        joinButton.interactable = false;

        connectionStatusText.text = "Joining Room...";

        await networkRunnerHandler.JoinRoom(joinRoomCodeInputField.text);

        roomCreateJoinPanel.SetActive(false);
        roomPlayerPanel.SetActive(true);

        connectionStatusText.text = "Joined Room Successfully!";
    }


    private void UpdatePlayerListOnJoin(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            hostStartButton.gameObject.SetActive(true);
        }

        PlayerNameUI playerNameUI = Instantiate(playerNameUIPref, playerNamesParentTransform);
        int playerId = player.PlayerId;
        playerNameUI.transform.SetSiblingIndex(playerId - 1);
        playerNameUI.SetPlayerName(playerId.ToString());

        connectedPlayerUIs.Add(playerId, playerNameUI);
    }

    private void UpdatePlayerListOnLeave(NetworkRunner runner, PlayerRef player)
    {
        Destroy(connectedPlayerUIs[player.PlayerId]);
        connectedPlayerUIs.Remove(player.PlayerId);
    }
}
