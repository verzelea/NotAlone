using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private GameManager gameManager;
    ChangeStatutGame changeStatutGame;

    public async Task AddStartButtonAsync()
    {
        gameManager = GetComponent<GameManager>();
        startButton = gameObject.transform.Find("LobbyCanvas/StartButton").GetComponent<Button>();
        startButton.gameObject.SetActive(true);
        
        await FunctionStartButtonAsync();
    }

    private async Task FunctionStartButtonAsync()
    {
        Player local;
        bool checkGetvalue = gameManager.GetPlayers().TryGetValue(gameManager.localPlayer, out local);
        while (!checkGetvalue)
        {
            await Task.Delay(25);
            checkGetvalue = gameManager.GetPlayers().TryGetValue(gameManager.localPlayer, out local);
        }
        changeStatutGame = local.gameObject.GetComponent<ChangeStatutGame>();
        startButton.onClick.AddListener(StartGameCliked);
    }

    private void StartGameCliked()
    {
        changeStatutGame.LaunchGame();
    }
}
