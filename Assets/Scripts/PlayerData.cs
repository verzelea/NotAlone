using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{
    public string player = "";

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer)
        {
            SetPlayer("player " + Random.Range(1, 100));
        }
    }

    public void SetPlayer(string newname)
    {
        player = newname;
    }

    public string GetPlayer()
    {
        return player;
    }
}
