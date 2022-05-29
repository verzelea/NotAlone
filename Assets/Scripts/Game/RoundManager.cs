using Mirror;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Image tokenSurvivor;

    [SerializeField]
    private Image tokenMonster;

    GameManager gameManager;

    VictoryManager victoryManager;

    private bool waitSetMonster = false;

    string idMonster;
    private int? tempPointMonster;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        victoryManager = GetComponent<VictoryManager>();
        tokenSurvivor = gameObject.transform.Find("GameCanvas/Token/TokenSurvivor").GetComponent<Image>();
        tokenMonster = gameObject.transform.Find("GameCanvas/Token/TokenMonster").GetComponent<Image>();
    }

    public async Task<Round> InitGameAsync(Dictionary<string, Player> players)
    {
        if (!NetworkClient.ready)
        {
            NetworkClient.Ready();
        }
        tokenSurvivor.enabled = false;
        tokenMonster.enabled = false;
        RandomMonster(players);
        while (waitSetMonster)
        {
            await Task.Delay(25);
        }
        victoryManager.ResetManager();
        Phase1();
        return Round.Survior;
    }

    private void RandomMonster(Dictionary<string, Player> players)
    {
        waitSetMonster = true;
        if (!gameManager.isServer)
        {
            return;
        }
        int random = new System.Random().Next(players.Count)+1;
        idMonster = "" + random;

        foreach (Player player in players.Values)
        {
            player.GetComponent<MonsterManager>().SetMonster(random);
        }        
    }

    public void SetMonster(int random)
    {
        foreach (Player player in gameManager.GetPlayers().Values)
        {
            player.data.IsMonster = random == player.netId;
        }

        if (gameManager.localPlayer == "" + random)
        {
            tokenMonster.enabled = true;
        }
        else
        {
            tokenSurvivor.enabled = true;
        }
        waitSetMonster = false;
    }

    private bool CheckLocation()
    {
        foreach(Player player in gameManager.GetPlayers().Values)
        {
            if (!player.data.Location.HasValue)
            {
                return false;
            }
        }
        return true;
    }

    public void SetTempPointMonster(int value)
    {
        tempPointMonster = value;
    }

    public void SetPlayerReadyToFalse()
    {
        var players = gameManager.GetPlayers();
        foreach(Player p in players.Values)
        {
            p.data.IsReady = false;
        }
    }

    public void SetPlayerReadyToTrue()
    {
        var players = gameManager.GetPlayers();
        foreach (Player p in players.Values)
        {
            p.data.IsReady = true;
        }
    }

    public void Phase4(Dictionary<string, Player> players)
    {
        //Debug.Log("Phase 4");
        victoryManager.AddPointPlayer(1);

        foreach ((string key, Player player) in players)
        {
            player.data.Location = null;
            if (key == gameManager.localPlayer)
            {
                player.ResetButton();
            }
        }
        SetPlayerReadyToTrue();
    }

    public async Task Phase3Async()
    {
        
        if (gameManager.isServer)
        {
            await Phase3ServeurAsync();
        }
        else
        {
            Phase3Client();
        }
        
        while (!tempPointMonster.HasValue)
        {
           await Task.Delay(25);
        }
        victoryManager.AddPointMonstre(tempPointMonster.Value);
        tempPointMonster = null;
        SetPlayerReadyToTrue();
    }

    private void Phase3Client()
    {
        var players = gameManager.GetPlayers();
        Player local = players.GetValueOrDefault(gameManager.localPlayer);

        if (!local.isServer)
        {
            local.SendLocationCmd(local.data.Location.Value);
        }
    }

    private async Task Phase3ServeurAsync()
    {
        while (!CheckLocation())
        {
            await Task.Delay(25);
        }
        var players = gameManager.GetPlayers();
        Player local = players.GetValueOrDefault(gameManager.localPlayer);
        
        LocationEnum monsterLocation = players.GetValueOrDefault(idMonster).data.Location.Value;
        int pointMonster = 0;
        foreach (Player player in players.Values)
        {
            if (player.data.IsMonster)
            {
                continue;
            }
            if (player.data.Location == monsterLocation)
            {
                pointMonster++;
            }
        }
        local.SendPointMonster(pointMonster);
    }


    [Client]
    public void Phase2()
    {
        var players = gameManager.GetPlayers();
        //Debug.Log("Phase 2");
        foreach ((string key, Player player) in players)
        {
            if (player.data.IsMonster)
            {
                player.data.IsReady = false;
                if (key == gameManager.localPlayer)
                {
                    player.ShowButton(true);
                }
            }
            else
            {
                player.data.IsReady = true;
            }
        }
    }

    [Client]
    public void Phase1()
    {
        var players = gameManager.GetPlayers();
        foreach ((string key, Player player) in players)
        {
            if (player.data.IsMonster)
            {
                player.data.IsReady = true;
            }
            else
            {
                player.data.IsReady = false;

                if(key == gameManager.localPlayer)
                {
                    player.ShowButton(false);
                }
            }
        }
    }
}
