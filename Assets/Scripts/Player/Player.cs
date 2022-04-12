using UnityEngine;

public class Player : MonoBehaviour
{
    public string id = "";
    public string player = "";
    public bool isMonster = false;


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
}
