using Mirror;
using UnityEngine;

public class ChangeStatutGame : NetworkBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    //Set the scene of each clients for start the game
    [ClientRpc]
    public void LaunchGame()
    {
        gameManager.SetIsGame(true);
    }

    //Set the scene of each clients for return on the lobby
    [ClientRpc]
    public void StopGame(Player player)
    {
        player.SetLocation(null);
        player.ResetButton();
        gameManager.SetIsGame(false);
    }
}
