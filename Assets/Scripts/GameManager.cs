using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string playerPrefix = "Player";
    public bool isGame = false;
    public string localPlayer;
    public bool isServer;

    private Dictionary<string, Player> players = new Dictionary<string, Player>();
    private Round etape;

    [SerializeField]
    private GameObject LobbyCanvas;
    [SerializeField]
    private GameObject GameCanvas;

    RoundManager roundManager;
    private Scene? scene = null;
    public bool sceneIsLoading = false;

    void Start()
    {
        LobbyCanvas = gameObject.transform.Find("LobbyCanvas").gameObject;
        GameCanvas = gameObject.transform.Find("GameCanvas").gameObject;
        LobbyCanvas.SetActive(true);
        roundManager = GetComponent<RoundManager>();
    }

    private void Update()
    {
        if (!isGame || !CheckAllPlayerReady())
        {
            return;
        }

        etape = EnumStatic.Next(etape);                
        switch (etape)
        {
            case Round.Survior:
                roundManager.SurvivorSetUp(players);
                break;
            case Round.Monster:
                roundManager.MonsterSetUp(players);
                break;
            case Round.Resolve:
                roundManager.ResolveSetUp(players);
                break;
            case Round.Reset:
                roundManager.ResetSetUp(players);
                break;
        }        
    }

    public void StartGame()
    {
        etape = roundManager.InitGame(players);
        Show();
    }

    public void RegisterPlayer(string netId, Player player, bool isLocalPlayer, bool isServer)
    {
        string name = playerPrefix + netId;
        player.SetPlayer(name);
        player.SetId(netId);
        players.Add(netId, player);
        if (isLocalPlayer)
        {
            localPlayer = netId;
        }
        this.isServer = isServer;

        Show();
    }

    public Dictionary<string, Player> GetPlayers() => players;

    //Delete
    public void Show()
    {
        string affiche = "";
        foreach ((string p, Player e) in players)
        {
            affiche += p + " : "+ e.data.IsReady  +"  ";
        }
        Debug.Log(affiche);
    }

    public int CountPlayer()
    {
        return players.Count;
    }

    private bool CheckAllPlayerReady()
    {
        bool isAllPlayerReady = true;
        foreach (Player player in players.Values)
        {
            if (!player.data.IsReady)
            {
                isAllPlayerReady = false;
            }
        }
        return isAllPlayerReady;
    }

    public void SetPlayerReady(string id, bool change)
    {
        Debug.Log("player " + id + " is " + change);
        players[id].data.IsReady = change;
    }
}
