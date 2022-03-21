using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerSetup : NetworkBehaviour
{
    private StartButton button = null;

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

        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        LobbyManager.RegisterPlayer(netId, player);

        scene = SceneManager.GetActiveScene();

        if (isServer && scene.name == "Lobby")
        {
            button = GameObject.Find("LobbyManager").GetComponent<StartButton>();
            button.AddStartButton();
            button.FunctionStartButton();
        }
    }
}
