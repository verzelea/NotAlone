using Mirror;
using UnityEngine;

public partial class Player : NetworkBehaviour
{
    private string id;
    private PlayerData data = new PlayerData();

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

    public void SetLocation(LocationEnum? location)
    {
        data.Location = location;
    }

    public LocationEnum? GetLocation()
    {
        return data.Location;
    }

    public void SetIsMonster(bool change)
    {
        data.IsMonster = change;
    }

    public bool GetIsMonster()
    {
        return data.IsMonster;
    }

    public void SetIsReady(bool change)
    {
        data.IsReady = change;
    }

    public bool GetIsReady()
    {
        return data.IsReady;
    }
}
