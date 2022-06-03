using Mirror;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public partial class RoundManager : MonoBehaviour
{
    private Image tokenSurvivor;
    private Image tokenMonster;
    private GameManager gameManager;
    private VictoryManager victoryManager;
    private UpdateChat updateChat;

    private bool waitSetMonster = false;
    private string idMonster;
    private int? tempPointMonster;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        victoryManager = GetComponent<VictoryManager>();
        updateChat = GetComponent<UpdateChat>();
        tokenSurvivor = gameObject.transform.Find("GameCanvas/Token/TokenSurvivor").GetComponent<Image>();
        tokenMonster = gameObject.transform.Find("GameCanvas/Token/TokenMonster").GetComponent<Image>();
    }

    //Start the game inside the roundManager
    public async Task<Round> InitGameAsync(Dictionary<string, Player> players)
    {
        tokenSurvivor.enabled = false;
        tokenMonster.enabled = false;
        RandomMonster();
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

    //Set all the players's attibutes IsReady to true
    public void SendLocation()
    {
        var players = gameManager.GetPlayers();
        foreach (Player p in players.Values)
        {
            if (!p.GetIsMonster())
            {
                p.SendMesage("Server: " + p.GetPlayer() + " choose location " + EnumStatic.GetEnumDescription(p.GetLocation().Value));
            }
        }
        var monster = players.First(p => p.Value.GetIsMonster()).Value;
        monster.SendMesage("Server: Monster " + monster.GetPlayer() + " choose location " + EnumStatic.GetEnumDescription(monster.GetLocation().Value));
    }
}
