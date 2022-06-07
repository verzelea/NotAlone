using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button startButton;
    private GameManager gameManager;
    private ChangeStatutGame changeStatutGame;

    //Get value for function to start button, and set it active
    public async Task AddStartButtonAsync()
    {
        gameManager = GetComponent<GameManager>();
        startButton = gameObject.transform.Find("LobbyCanvas/StartButton").GetComponent<Button>();
        startButton.gameObject.SetActive(true);
        
        await FunctionStartButtonAsync();
    }

    //Add function to start button
    private async Task FunctionStartButtonAsync()
    {
        Player local;
        bool checkGetvalue = gameManager.GetPlayers().TryGetValue(gameManager.GetLocalPlayer(), out local);
        while (!checkGetvalue)
        {
            await Task.Delay(25);
            checkGetvalue = gameManager.GetPlayers().TryGetValue(gameManager.GetLocalPlayer(), out local);
        }
        changeStatutGame = local.gameObject.GetComponent<ChangeStatutGame>();
        startButton.onClick.AddListener(StartGameCliked);
    }

    //Launch the game
    private void StartGameCliked()
    {
        changeStatutGame.LaunchGame();
    }
}
