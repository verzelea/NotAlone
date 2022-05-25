using Mirror;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundText;

    GameManager gameManager;

    VictoryManager victoryManager;

    private void Start()
    {
        roundText = gameObject.transform.Find("GameCanvas/RoundBox/Text").GetComponent<TextMeshProUGUI>();
        gameManager = GetComponent<GameManager>();
        victoryManager = GetComponent<VictoryManager>();
    }

    public Round InitGame(Dictionary<string, Player> players)
    {
        if (!NetworkClient.ready)
        {
            NetworkClient.Ready();
        }
        RandomMonster(players);
        roundText.text = EnumStatic.GetEnumDescription(Round.Survior);
        SurvivorSetUp(players);
        return Round.Survior;
    }

    private void RandomMonster(Dictionary<string, Player> players)
    {
        if (!gameManager.isServer)
        {
            return;
        }
        int random = new System.Random().Next(players.Count);
        Debug.Log("random = "+random);
        players.TryGetValue(gameManager.localPlayer, out Player local);
        local.SetMonsterClient(random);
    }

    public void SetMonster(int choose)
    {
        var players = gameManager.GetPlayers();
        foreach (Player player in players.Values)
        {
            if (choose == 0)
            {
                player.data.IsMonster = true;
            }
            else
            {
                player.data.IsMonster = false;
            }
            choose--;
        }
    }

    public void ChangeText(Round etape)
    {
        roundText.text = EnumStatic.GetEnumDescription(etape);
    }

    public void ResetSetUp(Dictionary<string, Player> players)
    {
        Debug.Log("Phase 4");
        victoryManager.AddPointPlayer(1);

        players.TryGetValue(gameManager.localPlayer, out Player local);
        local.ResetButton();
    }

    public void ResolveSetUp(Dictionary<string, Player> players)
    {
        Debug.Log("Phase 3");
    }

    public void MonsterSetUp(Dictionary<string, Player> players)
    {
        Debug.Log("Phase 2");
        foreach (Player player in players.Values)
        {
            if (!player.data.IsMonster)
            {
                player.data.IsReady = true;
            }
            else
            {
                player.data.IsReady = false;
            }
        }

        if (!NetworkClient.ready)
        {
            NetworkClient.Ready();
        }
        players.TryGetValue(gameManager.localPlayer, out Player local);
        local.ShowButton(true);
    }

    [Client]
    public void SurvivorSetUp(Dictionary<string, Player> players)
    {
        Debug.Log("Phase 1");
        foreach (Player player in players.Values)
        {
            if (player.data.IsMonster)
            {
                player.data.IsReady = true;
            }
            else
            {
                player.data.IsReady = false;
            }
        }

        if (!NetworkClient.ready)
        {
            NetworkClient.Ready();
        }
        players.TryGetValue(gameManager.localPlayer, out Player local);
        local.ShowButton(false);
    }
}
