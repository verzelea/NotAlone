using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    public void AddStartButton()
    {
        startButton = gameObject.transform.Find("LobbyCanvas/StartButton").GetComponent<Button>();
        startButton.gameObject.SetActive(true);
        FunctionStartButton();
    }

    private void FunctionStartButton()
    {
        startButton.onClick.AddListener(StartGameCliked);
    }

    private void StartGameCliked()
    {
        NetworkManager.singleton.ServerChangeScene("Game");
        GetComponent<NewGameManager>().SetupGame();
    }
}
