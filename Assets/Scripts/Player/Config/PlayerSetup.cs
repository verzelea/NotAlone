using Mirror;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    private StartButton startButton;
    private VictoryManager victoryManager;
    private GameManager gameManager;
    private GameObject manager;
    private Transform locationCanvas;
    private LocationManager locationManager;

    [SerializeField]
    private GameObject objectToDelete;

    private bool? keepIsGame;

    // Start is called before the first frame update, when Client come on Server
    public override void OnStartClient()
    {
        if (!isLocalPlayer)
        {
            objectToDelete.SetActive(false);
        }
        manager = GameObject.Find("GameManager");
        locationCanvas = gameObject.transform.Find("PlayerCanvas/LocationUI");
        locationManager = GetComponent<LocationManager>();
        locationManager.SetUpLocations(locationCanvas);
        victoryManager = manager.GetComponent<VictoryManager>();
        gameManager = manager.GetComponent<GameManager>();
        startButton = manager.GetComponent<StartButton>();

        AddPlayer();
    }

    //Add the new player into the gameManager
    private void AddPlayer()
    {
        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        gameManager.RegisterPlayer(netId, player, isLocalPlayer, isServer);
    }

    //Set the player with the change in the scene (Game is launch or not)
    private async void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if(keepIsGame.HasValue && keepIsGame == gameManager.GetIsGame())
        {
            return;
        }
        keepIsGame = gameManager.GetIsGame();

        if (!keepIsGame.Value)
        {
            SetupLobby();
            if (isServer)
            {
                await SetupLobbyServerAsync();
            }
            CloseGame();
        }
        else
        {
            SetupGame();
            if (isServer)
            {
                SetupGameServer();
            }
        }
    }

    //Set the player for the lobby
    private void SetupLobby()
    {
        var chat = gameObject.transform.Find("PlayerCanvas/ChatUI").gameObject;
        chat.SetActive(true);
    }

    //Set the lobby for the serveur
    private async Task SetupLobbyServerAsync()
    {
        await startButton.AddStartButtonAsync();
    }

    //Set the game
    private void SetupGame()
    {
        if (isLocalPlayer)
        {
            locationCanvas.gameObject.SetActive(true);
        }
    }

    //Stop the game
    private void CloseGame()
    {
        if (isLocalPlayer)
        {
            locationCanvas.gameObject.SetActive(false);
        }
    }

    //Set game for the serveur
    private void SetupGameServer()
    {
        victoryManager.AddReturnButton();
    }
}
