using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundText;

    private void Start()
    {
        roundText = gameObject.transform.Find("GameCanvas/RoundBox/Text").GetComponent<TextMeshProUGUI>();
    }

    public Round InitGame(Dictionary<string, PlayerData> players)
    {
        RandomMonster(players);
        roundText.text = EnumStatic.GetEnumDescription(Round.Survior);
        SurvivorSetUp(players);
        return Round.Survior;
    }

    private void RandomMonster(Dictionary<string, PlayerData> players)
    {
        int random = new System.Random().Next(players.Count);
        foreach (PlayerData player in players.Values)
        {
            if (random == 0)
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

    public void ChangeText(Round etape)
    {
        roundText.text = EnumStatic.GetEnumDescription(etape);
    }

    public void ResetSetUp()
    {
        GetComponent<VictoryManager>().AddPointPlayer(1);
    }

    public void ResolveSetUp()
    {

    }

    public void MonsterSetUp(Dictionary<string, PlayerData> players)
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

    public void SurvivorSetUp(Dictionary<string, PlayerData> players)
    {
        foreach (PlayerData player in players.Values)
        {
            if (!player.IsMonster)
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
