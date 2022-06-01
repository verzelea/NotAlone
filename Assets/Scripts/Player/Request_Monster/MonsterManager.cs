using Mirror;
using UnityEngine;

public class MonsterManager : NetworkBehaviour
{
    private RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
    }

    //Tell to each clients that the player with id equal to parameter choose
    [Client]
    public void SetMonster(int choose)
    {
        if (isServer)
        {
            SetMonsterRpc(choose);
        }
        else
        {
            SetMonsterCmd(choose);
        }
    }

    //The server send the request for all the clients
    [Command]
    private void SetMonsterCmd(int choose)
    {
        SetMonsterRpc(choose);
    }

    //Each clients set the attribute IsMonster for each players
    [ClientRpc]
    private void SetMonsterRpc(int choose)
    {
        roundManager.SetMonster(choose);
    }
}
