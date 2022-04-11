using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private const string playerPrefix = "Player";

    private Dictionary<string, Player> players = new Dictionary<string, Player>();

    public void RegisterPlayer(string netId, Player player)
    {
        Debug.Log(netId);
        string name = playerPrefix + netId;
        players.Add(name, player);
        player.SetPlayer(name);
        player.SetId(netId);
    }

    public void CleanPlayer()
    {
        players = new Dictionary<string, Player>();
    }

    public int CountPlayer()
    {
        return players.Count;
    }
}
