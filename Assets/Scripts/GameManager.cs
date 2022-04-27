using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    private const string playerPrefix = "Player";
    private bool isGame = false;

    private Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();
    private Round etape;

    [SerializeField]
    private GameObject LobbyCanvas;
    [SerializeField]
    private GameObject GameCanvas;

    RoundManager roundManager;

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
                roundManager.ResolveSetUp();
                break;
            case Round.Reset:
                roundManager.ResetSetUp();
                break;
        }
        
        //GetComponent<VictoryManager>().AddPointPlayer(1);
        
    }

    public void SetupGame()
    {
        GameCanvas.SetActive(true);
        LobbyCanvas.SetActive(false);
        isGame = true;
        etape = roundManager.InitGame(players);
    }

    public void SetupLobby()
    {
        var endScreen = gameObject.transform.Find("EndCanvas").gameObject;
        endScreen.SetActive(false);
        GameCanvas.SetActive(false);
        LobbyCanvas.SetActive(true);
        isGame = false;
    }

    public void RegisterPlayer(string netId, Player player)
    {
        string name = playerPrefix + netId;
        player.SetPlayer(name);
        //Debug.Log(player.data.Player);
        player.SetId(netId);
        players.Add(netId, player.data);
    }

    public int CountPlayer()
    {
        return players.Count;
    }

    private bool CheckAllPlayerReady()
    {
        bool isAllPlayerReady = true;
        foreach (PlayerData player in players.Values)
        {
            if (!player.IsReady)
            {
                isAllPlayerReady = false;
            }
        }
        return isAllPlayerReady;
    }
}
