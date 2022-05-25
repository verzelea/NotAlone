using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    ReadyButton readyButton;
    MonsterManager monsterManager;

    public string id;
    public PlayerData data = new PlayerData();

    private void Start()
    {
        readyButton = GetComponent<ReadyButton>();
        monsterManager = GetComponent<MonsterManager>();
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

    [Client]
    public void SetMonsterClient(int choose)
    {
        Debug.Log("Enter Client with " + choose);
        if (!isServer)
        {
            return;
        }
        monsterManager.SetMonsterRpc(choose);
    }
}
