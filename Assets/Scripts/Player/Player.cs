using UnityEngine;

public class Player : MonoBehaviour
{
    private string id = "";
    private string player = "";
    private bool isMonster = false;
    public LocationEnum? location = null;

    public void SetPlayer(string newname)
    {
        player = newname;
    }

    public string GetPlayer()
    {
        return player;
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
        this.location = location;
        Debug.Log("location = " + this.location);
    }

    public LocationEnum? GetLocation()
    {
        return location;
    }
}
