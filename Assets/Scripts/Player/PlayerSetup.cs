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

        if (scene.name == "Lobby")
        {
            SetupLobby();
            if (isServer)
            {
                SetupLobbyServer();
            }
        }

        if (scene.name == "Game")
        {
            SetupGameServer();
            if (isServer)
            {
                SetupGameServer();
            }
        }
    }

    private void SetupLobby()
    {
        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
        lobbyManager.RegisterPlayer(netId, player);

        Debug.Log("Coucou");
        var chat = gameObject.transform.Find("PlayerCanvas/ChatUI").gameObject;
        chat.SetActive(true);
    }

    private void SetupLobbyServer()
    {
        startButton = GameObject.Find("LobbyManager").GetComponent<StartButton>();
        startButton.AddStartButton();
    }

    private void SetupGame()
    {

    }

    private void SetupGameServer()
    {
        returnButton = GameObject.Find("GameManager").GetComponent<VictoryManager>();
        returnButton.AddReturnButton();
    }
}
