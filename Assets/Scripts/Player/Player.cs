using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    ReadyButton readyButton;

    public string id;
    public PlayerData data = new PlayerData();
    [SerializeField]
    private GameManager gameManager;

    private void Start()
    {
        readyButton = GetComponent<ReadyButton>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetPlayer(string newname)
    {
        data.Player = newname;
    }

    public string GetPlayer()
    {
        return data.Player;
    }

    public void SetId(string newid)
    {
        id = newid;
    }

    public string GetId()
    {
        return id;
    }

    public void SetLocation(LocationEnum location)
    {
        data.Location = location;
    }

    public LocationEnum? GetLocation()
    {
        return data.Location;
    }

    public void ShowButton(bool isMonsterOrNot)
    {
        if (isLocalPlayer)
        {
            readyButton.ShowButton(isMonsterOrNot);
        }
    }

    public void ResetButton()
    {
        readyButton.ResetButton();
    }

    public void SetIsMonster(bool change)
    {
        data.IsMonster = change;
    }

    [Command]
    public void SendLocationCmd(LocationEnum location)
    {
        gameManager.SetPlayerLocation(netId.ToString(), location);
    }

    [ClientRpc]
    public void SendPointMonster(int pointMonster)
    {
        GameObject.Find("GameManager").GetComponent<RoundManager>().SetTempPointMonster(pointMonster);
    }
}
