using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string playerPrefix = "Player";
    public bool isGame = false;

    private Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();
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
                roundManager.ResolveSetUp();
                break;
            case Round.Reset:
                roundManager.ResetSetUp();
                break;
        }        
    }

    public void StartGame()
    {
        etape = roundManager.InitGame(players);
    }

    public void RegisterPlayer(string netId, Player player)
    {
        string name = playerPrefix + netId;
        player.SetPlayer(name);
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

    public void SetPlayerReady(string id, bool change)
    {
        Debug.Log("player " + id + " is " + change);
        players[id].IsReady = change;
    }
}
