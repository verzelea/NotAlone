using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

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
    public bool sceneIsLoading = false;

    [SerializeField]
    private TextMeshProUGUI roundText;

    void Start()
    {
        LobbyCanvas = gameObject.transform.Find("LobbyCanvas").gameObject;
        GameCanvas = gameObject.transform.Find("GameCanvas").gameObject;
        LobbyCanvas.SetActive(true);
        roundManager = GetComponent<RoundManager>();
        roundText = gameObject.transform.Find("GameCanvas/RoundBox/Text").GetComponent<TextMeshProUGUI>();
    }

    private async void Update()
    {
        if (!isGame || !CheckAllPlayerReady())
        {
            return;
        }

        etape = EnumStatic.Next(etape);
        roundText.text = EnumStatic.GetEnumDescription(etape);
        roundManager.SetPlayerReadyToFalse();
        switch (etape)
        {
            case Round.Survior:
                roundManager.Phase1();
                break;
            case Round.Monster:
                roundManager.Phase2();
                break;
            case Round.Resolve:
                await roundManager.Phase3Async();
                break;
            case Round.Reset:
                roundManager.Phase4(players);
                break;
        }
    }

    public async void StartGame()
    {
        etape = await roundManager.InitGameAsync(players);
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
    }

    public Dictionary<string, Player> GetPlayers() => players;

    //Delete
    public void Show(string source, Dictionary<string, Player> pp)
    {
        string affiche = "";
        foreach ((string k, Player e) in pp)
        {
            affiche += k + " : "+ e.data.IsReady  +"  ";
        }
        Debug.Log(source + " " + affiche);
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
        players[id].data.IsReady = change;
    }

    public void SetPlayerLocation(string id, LocationEnum change)
    {
        players[id].data.Location = change;
    }
}
