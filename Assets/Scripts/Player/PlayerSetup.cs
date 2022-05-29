using Mirror;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    private StartButton startButton;
    private VictoryManager victoryManager;

    private GameManager gameManager;

    [SerializeField]
    GameObject objectToDelete;

    GameObject manager;

    Transform locationCanvas;

    LocationManager locationManager;

    public bool? keepIsGame;

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
        locationManager.SetUpLocations(manager, locationCanvas);
        victoryManager = manager.GetComponent<VictoryManager>();
        gameManager = manager.GetComponent<GameManager>();
        startButton = manager.GetComponent<StartButton>();

        AddPlayer();
    }

    private void AddPlayer()
    {
        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        gameManager.RegisterPlayer(netId, player, isLocalPlayer, isServer);
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if(keepIsGame.HasValue && keepIsGame == gameManager.isGame)
        {
            return;
        }
        keepIsGame = gameManager.isGame;

        if (!keepIsGame.Value)
        {
            SetupLobby();
            if (isServer)
            {
                SetupLobbyServerAsync();
            }
            CloseGame();
        }
        else
        {
            SetupGame();
            if (isServer)
            {
                SetupGameServer();
                //CloseLobbyServer(manager);
            }
        }
    }

    private void SetupLobby()
    {
        var chat = gameObject.transform.Find("PlayerCanvas/ChatUI").gameObject;
        chat.SetActive(true);
    }

    private async Task SetupLobbyServerAsync()
    {
        await startButton.AddStartButtonAsync();
    }

    private void SetupGame()
    {
        if (isLocalPlayer)
        {
            locationCanvas.gameObject.SetActive(true);
        }
    }

    private void CloseGame()
    {
        if (isLocalPlayer)
        {
            locationCanvas.gameObject.SetActive(false);
        }
    }

    private void SetupGameServer()
    {
        victoryManager.AddReturnButton();
    }
}
