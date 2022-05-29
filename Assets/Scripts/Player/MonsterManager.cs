using Mirror;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : NetworkBehaviour
{
    RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
    }

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

    [Command]
    private void SetMonsterCmd(int choose)
    {
        SetMonsterRpc(choose);
    }

    [ClientRpc]
    private void SetMonsterRpc(int choose)
    {
        roundManager.SetMonster(choose);
    }
}
