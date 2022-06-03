using System.Collections.Generic;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    private const string playerPrefix = "Player";

    private bool isGame = false;
    private string localPlayer;
    private bool isServer;
    private bool sceneIsLoading = false;

    private Dictionary<string, Player> players = new Dictionary<string, Player>();
    private Round etape;

    //Get the list of all players
    public Dictionary<string, Player> GetPlayers() => players;

    //Number of players in the game
    public int CountPlayer()
    {
        return players.Count;
    }

    //Change the attribute IsReady, with the param change, from the player with the param id equals to is netId
    public void SetPlayerReady(string id, bool change)
    {
        players[id].SetIsReady(change);
    }

    //Change the location, with the param change, of the player with the param id equals to is netId
    public void SetPlayerLocation(string id, LocationEnum change)
    {
        players[id].SetLocation(change);
    }

    //Change the game status(true => launch the game, false => stop the game)
    public void SetIsGame(bool change)
    {
        isGame = change;
    }

    //Get the current status of the game
    public bool GetIsGame()
    {
        return isGame;
    }

    //Change the local player (to find quickly the local player in the list of players)
    public void SetLocalPlayer(string change)
    {
        localPlayer = change;
    }

    //Get the id of the local player
    public string GetLocalPlayer()
    {
        return localPlayer;
    }

    //Change the server (to find quickly if the client is the host)
    public void SetIsServer(bool change)
    {
        isServer = change;
    }

    //Get if the player is the host or not
    public bool GetIsServer()
    {
        return isServer;
    }

    //Set if the scene is changing or not
    public void SetSceneIsLoading(bool change)
    {
        sceneIsLoading = change;
    }

    //Get if the scene is changing or not
    public bool GetSceneIsLoading()
    {
        return sceneIsLoading;
    }
}
