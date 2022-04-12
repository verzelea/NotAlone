using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSetup : NetworkBehaviour
{
    private StartButton startButton = null;
    private VictoryManager returnButton = null;

    private LobbyManager lobbyManager = null;

    [SerializeField]
    GameObject objectToDelete;

    private Scene scene;

    // Start is called before the first frame update
    private void Start()
    {
        if (!isLocalPlayer || scene.name == "Game")
        {
            objectToDelete.SetActive(false);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        scene = SceneManager.GetActiveScene();
        GameObject manager;

        if (scene.name == "Lobby")
        {
            manager = GameObject.Find("LobbyManager");
            SetupLobby(manager);
            if (isServer)
            {
                SetupLobbyServer(manager);
            }
        }

        if (scene.name == "Game")
        {
            manager = GameObject.Find("GameManager");
            SetupGame(manager);
            if (isServer)
            {
                SetupGameServer(manager);
            }
        }
    }

    private void SetupLobby(GameObject manager)
    {
        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        lobbyManager = manager.GetComponent<LobbyManager>();
        lobbyManager.RegisterPlayer(netId, player);

        var chat = gameObject.transform.Find("PlayerCanvas/ChatUI").gameObject;
        chat.SetActive(true);
    }

    private void SetupLobbyServer(GameObject manager)
    {
        startButton = manager.GetComponent<StartButton>();
        startButton.AddStartButton();
    }

    private void SetupGame(GameObject manager)
    {
        var objectButton = gameObject.transform.Find("PlayerCanvas/LocationUI");
        objectButton.gameObject.SetActive(true);

        var locationManager = GetComponent<LocationManager>();
        locationManager.SetUpLocations(manager, objectButton);
    }

    private void SetupGameServer(GameObject manager)
    {
        returnButton = manager.GetComponent<VictoryManager>();
        returnButton.AddReturnButton();
    }
}
