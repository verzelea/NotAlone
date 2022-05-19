using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private GameManager gameManager;

    public void AddStartButton()
    {
        startButton = gameObject.transform.Find("LobbyCanvas/StartButton").GetComponent<Button>();
        startButton.gameObject.SetActive(true);
        FunctionStartButton();
        gameManager = GetComponent<GameManager>();
    }

    private void FunctionStartButton()
    {
        startButton.onClick.AddListener(StartGameCliked);
    }

    private void StartGameCliked()
    {
        if (!gameManager.sceneIsLoading)
        {
            gameManager.sceneIsLoading = true;
            NetworkManager.singleton.ServerChangeScene("Game");
        }
    }
}
