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
            string netId = GetComponent<NetworkIdentity>().netId.ToString();
            Player player = GetComponent<Player>();
            lobbyManager = GameObject.Find("LobbyManager").GetComponent<LobbyManager>();
            lobbyManager.RegisterPlayer(netId, player);
        }

        if (isServer && scene.name == "Lobby")
        {
            startButton = GameObject.Find("LobbyManager").GetComponent<StartButton>();
            startButton.AddStartButton();
        }

        if (isServer && scene.name == "Game")
        {
            returnButton = GameObject.Find("GameManager").GetComponent<VictoryManager>();
            returnButton.AddReturnButton();
        }
    }
}
