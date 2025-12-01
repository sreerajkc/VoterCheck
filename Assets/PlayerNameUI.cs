using TMPro;
using UnityEngine;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;

    public void SetPlayerName(string playerName)
    {
        nameText.text = playerName;
    }
}
