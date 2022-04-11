using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, Player> players = new Dictionary<string, Player>();

    public int CountPlayer()
    {
        return players.Count;
    }
}
