using Mirror;
using UnityEngine;

public partial class Player : NetworkBehaviour
{
    private RoundManager roundManager;
    private ReadyButton readyButton;
    private GameManager gameManager;

    private void Start()
    {
        readyButton = GetComponent<ReadyButton>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
    }

    //Make the button for validate the location apparent
    public void ShowButton(bool isMonsterOrNot)
    {
        if (isLocalPlayer)
        {
            readyButton.ShowButton(isMonsterOrNot);
        }
    }

    //Reset the ready button (remove the validation image and make the button hidden
    public void ResetButton()
    {
        readyButton.ResetButton();
    }

    //Send the location for the host client
    [Command]
    public void SendLocationCmd(LocationEnum location)
    {
        gameManager.SetPlayerLocation(netId.ToString(), location);
    }

    //Send the number of survivor find by the monster
    [ClientRpc]
    public void SendPointMonster(int pointMonster)
    {
        roundManager.SetTempPointMonster(pointMonster);
    }
}

