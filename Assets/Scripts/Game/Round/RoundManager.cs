using Mirror;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    private Image tokenSurvivor;
    private Image tokenMonster;
    private GameManager gameManager;
    private VictoryManager victoryManager;

    private bool waitSetMonster = false;
    private string idMonster;
    private int? tempPointMonster;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        victoryManager = GetComponent<VictoryManager>();
        tokenSurvivor = gameObject.transform.Find("GameCanvas/Token/TokenSurvivor").GetComponent<Image>();
        tokenMonster = gameObject.transform.Find("GameCanvas/Token/TokenMonster").GetComponent<Image>();
    }

    //Start the game inside the roundManager
    public async Task<Round> InitGameAsync(Dictionary<string, Player> players)
    {
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

    //Choose randomly one player to be the monster
    private void RandomMonster()
    {
        var players = gameManager.GetPlayers();
        waitSetMonster = true;
        if (!gameManager.GetIsServer())
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

    //Change data of all players to say if he's the monster or not
    public void SetMonster(int random)
    {
        foreach (Player player in gameManager.GetPlayers().Values)
        {
            player.SetIsMonster(random == player.netId);
        }

        if (gameManager.GetLocalPlayer() == "" + random)
        {
            tokenMonster.enabled = true;
        }
        else
        {
            tokenSurvivor.enabled = true;
        }
        waitSetMonster = false;
    }

    //Check if all players have chosen a location
    private bool CheckLocation()
    {
        foreach(Player player in gameManager.GetPlayers().Values)
        {
            if (!player.GetLocation().HasValue)
            {
                return false;
            }
        }
        return true;
    }

    //send how many point the monster get this round
    public void SetTempPointMonster(int value)
    {
        tempPointMonster = value;
    }

    //Set all the players attibutes IsReady to false
    public void SetPlayerReadyToFalse()
    {
        var players = gameManager.GetPlayers();
        foreach(Player p in players.Values)
        {
            p.SetIsReady(false);
        }
    }

    //Set all the players's attibutes IsReady to true
    public void SetPlayerReadyToTrue()
    {
        var players = gameManager.GetPlayers();
        foreach (Player p in players.Values)
        {
            p.SetIsReady(true);
        }
    }

    //Make the code for round 4
    public void Phase4(Dictionary<string, Player> players)
    {
        victoryManager.AddPointPlayer(1);

        foreach ((string key, Player player) in players)
        {
            player.SetLocation(null);
            if (key == gameManager.GetLocalPlayer())
            {
                player.ResetButton();
            }
        }
        SetPlayerReadyToTrue();
    }

    //Make the code for round 3
    public async Task Phase3Async()
    {
        
        if (gameManager.GetIsServer())
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

    //Make the code for round 3 for all the clients (except host)
    private void Phase3Client()
    {
        var players = gameManager.GetPlayers();
        Player local = players.GetValueOrDefault(gameManager.GetLocalPlayer());

        if (!local.isServer)
        {
            local.SendLocationCmd(local.GetLocation().Value);
        }
    }

    //Make the code for round 3 for the host
    private async Task Phase3ServeurAsync()
    {
        while (!CheckLocation())
        {
            await Task.Delay(25);
        }
        var players = gameManager.GetPlayers();
        Player local = players.GetValueOrDefault(gameManager.GetLocalPlayer());
        
        LocationEnum monsterLocation = players.GetValueOrDefault(idMonster).GetLocation().Value;
        int pointMonster = 0;
        foreach (Player player in players.Values)
        {
            if (player.GetIsMonster())
            {
                continue;
            }
            if (player.GetLocation() == monsterLocation)
            {
                pointMonster++;
            }
        }
        local.SendPointMonster(pointMonster);
    }

    //Make the code for round 2
    [Client]
    public void Phase2()
    {
        var players = gameManager.GetPlayers();
        foreach ((string key, Player player) in players)
        {
            if (player.GetIsMonster())
            {
                player.SetIsReady(false);
                if (key == gameManager.GetLocalPlayer())
                {
                    player.ShowButton(true);
                }
            }
            else
            {
                player.SetIsReady(true);
            }
        }
    }

    //Make the code for round 1
    [Client]
    public void Phase1()
    {
        var players = gameManager.GetPlayers();
        foreach ((string key, Player player) in players)
        {
            if (player.GetIsMonster())
            {
                player.SetIsReady(true);
            }
            else
            {
                player.SetIsReady(false);

                if(key == gameManager.GetLocalPlayer())
                {
                    player.ShowButton(false);
                }
            }
        }
    }
}
