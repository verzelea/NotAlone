using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string playerPrefix = "Player";

    private Dictionary<string, PlayerData> players = new Dictionary<string, PlayerData>();
    private Round etape = Round.Survior;

    [SerializeField]
    TextMeshProUGUI roundText;

    public int CountPlayer()
    {
        return players.Count;
    }

    public void SetupGame()
    {

    }

    public void RegisterPlayer(string netId, Player player)
    {
        //Debug.Log(netId);
        string name = playerPrefix + netId;
        player.SetPlayer(name);
        Debug.Log(player.data.Player);
        player.SetId(netId);
        players.Add(netId, player.data);
    }

    private void Start()
    {
        RandomMonster();
        roundText.text = EnumStatic.GetEnumDescription(etape);
        SurvivorSetUp();
    }

    private void RandomMonster()
    {
        int random = new System.Random().Next(players.Count);
        foreach (PlayerData player in players.Values)
        {
            if(random == 0)
            {
                player.IsMonster = true;
            }
            else
            {
                player.IsMonster = false;
            }
            random--;
        }
    }

    private void Update()
    {
        if(CheckAllPlayerReady())
        {
            etape = EnumStatic.Next(etape);
            roundText.text = EnumStatic.GetEnumDescription(etape);
            switch (etape)
            {
                case Round.Survior :
                    SurvivorSetUp();
                    break;
                case Round.Monster :
                    MonsterSetUp();
                    break;
                case Round.Resolve :
                    ResolveSetUp();
                    break;
                case Round.Reset :
                    ResetSetUp();
                    break;
            }      
        }
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

    private void ResetSetUp()
    {
    }

    private void ResolveSetUp()
    {
    }

    private void MonsterSetUp()
    {
        foreach (PlayerData player in players.Values)
        {
            if (!player.IsMonster)
            {
                player.IsReady = true;
            }
            else
            {
                player.IsReady = false;
            }
        }
    }

    private void SurvivorSetUp()
    {
        foreach (PlayerData player in players.Values)
        {
            if(!player.IsMonster)
            {
                player.IsReady = false;
            }
            else
            {
                player.IsReady = true;
            }
        }
    }
}
