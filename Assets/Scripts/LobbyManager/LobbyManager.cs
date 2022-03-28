using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private const string playerPrefix = "Player";

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer(string netId, Player player)
    {
        string name = playerPrefix + netId;
        players.Add(name, player);
        player.SetPlayer(name);
        player.SetId(netId);
    }
}
