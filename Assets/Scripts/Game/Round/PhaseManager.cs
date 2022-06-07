using Mirror;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public partial class RoundManager : MonoBehaviour
{
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
        SendLocation();
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

                if (key == gameManager.GetLocalPlayer())
                {
                    player.ShowButton(false);
                }
            }
        }
    }
}
