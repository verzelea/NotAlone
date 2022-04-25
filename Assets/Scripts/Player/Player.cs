using UnityEngine;

public class Player : MonoBehaviour
{
    public string id;
    public PlayerData data = new PlayerData();

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
        Debug.Log(data.Player + " = " + data.Location);
    }

    public LocationEnum? GetLocation()
    {
        return data.Location;
    }
}
