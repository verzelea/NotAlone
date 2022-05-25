using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSetup : NetworkBehaviour
{
    private StartButton startButton;
    private VictoryManager victoryManager;

    private GameManager gameManager;

    [SerializeField]
    GameObject objectToDelete;

    GameObject manager;
    private Scene scene;

    Transform locationCanvas;

    LocationManager locationManager;

    // Start is called before the first frame update, when Client come on Server
    public override void OnStartClient()
    {
        if (!isLocalPlayer || scene.name == "Game")
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
        scene = SceneManager.GetActiveScene();

        if (scene.name == "Lobby")
        {
            SetupLobby();
            if (isServer)
            {
                SetupLobbyServer();
                
            }
            CloseGame();
        }

        if (scene.name == "Game")
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

    private void SetupLobbyServer()
    {
        startButton.AddStartButton();
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
