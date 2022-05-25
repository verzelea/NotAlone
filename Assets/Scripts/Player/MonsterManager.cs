using Mirror;
using UnityEngine;

public class MonsterManager : NetworkBehaviour
{
    RoundManager roundManager;

    // Start is called before the first frame update
    void Start()
    {
        roundManager = GameObject.Find("GameManager").GetComponent<RoundManager>();
    }

    /*[Client]
    public void SetMonsterClient(int choose)
    {
        Debug.Log("Enter Client with " + choose);
        if (!isServer)
        {
            return;
        }
        SetMonsterRpc(choose);
    }*/

    [Command]
    public void SetMonsterCmd(int choose)
    {
        Debug.Log("Enter Cmd with " + choose);
        SetMonsterRpc(choose);
    }

    [ClientRpc]
    public void SetMonsterRpc(int choose)
    {
        Debug.Log("Enter Rpc with " + choose);
        roundManager.SetMonster(choose);
    }
}
