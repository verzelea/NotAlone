using TMPro;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    private TextMeshProUGUI roundText;
    private GameObject LobbyCanvas;
    private GameObject GameCanvas;
    private RoundManager roundManager;
    

    void Start()
    {
        LobbyCanvas = gameObject.transform.Find("LobbyCanvas").gameObject;
        GameCanvas = gameObject.transform.Find("GameCanvas").gameObject;
        LobbyCanvas.SetActive(true);
        roundManager = GetComponent<RoundManager>();
        roundText = gameObject.transform.Find("GameCanvas/RoundBox/Text").GetComponent<TextMeshProUGUI>();
    }

    //If the game is launch and all the player are ready,
    //change the phase for the next
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

    //Init all the component for the game
    public async void StartGame()
    {
        etape = await roundManager.InitGameAsync(players);
    }

    //Add a player in the list of player
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

    //Check if all the players are ready
    private bool CheckAllPlayerReady()
    {
        bool isAllPlayerReady = true;
        foreach (Player player in players.Values)
        {
            if (!player.GetIsReady())
            {
                isAllPlayerReady = false;
            }
        }
        return isAllPlayerReady;
    }
}