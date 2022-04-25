using System.Collections.Generic;
using UnityEngine;

public class NewGameManager : MonoBehaviour
{
    private const string playerPrefix = "Player";
    private bool isGame = false;

    private Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();
    private Round etape = Round.Survior;

    [SerializeField]
    private GameObject LobbyCanvas;
    [SerializeField]
    private GameObject GameCanvas;

    void Start()
    {
        LobbyCanvas = gameObject.transform.Find("LobbyCanvas").gameObject;
        GameCanvas = gameObject.transform.Find("GameCanvas").gameObject;
        LobbyCanvas.SetActive(true);
    }

    private void Update()
    {
        if (isGame)
        {
            //GetComponent<VictoryManager>().AddPointPlayer(1);
        }
    }

    public void SetupGame()
    {
        GameCanvas.SetActive(true);
        LobbyCanvas.SetActive(false);
        isGame = true;
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
}
