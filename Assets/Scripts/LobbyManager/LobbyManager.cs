using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private const string playerPrefix = "Player";

    private Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();

    public void RegisterPlayer(string netId, Player player)
    {
        //Debug.Log(netId);
        string name = playerPrefix + netId;
        player.SetPlayer(name);
        Debug.Log(player.data.Player);
        player.SetId(netId);
        players.Add(netId, player.data);
    }

    public void CleanPlayer()
    {
        players = new Dictionary<string, PlayerData>();
    }

    public int CountPlayer()
    {
        return players.Count;
    }
}
