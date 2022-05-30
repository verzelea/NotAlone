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

    [ClientRpc]
    public void LaunchGame()
    {
        gameManager.isGame = true;
    }

    [ClientRpc]
    public void StopGame(Player player)
    {
        player.data.Location = null;
        player.ResetButton();
        gameManager.isGame = false;
    }
}
