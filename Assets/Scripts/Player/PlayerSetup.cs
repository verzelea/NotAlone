using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    //[SerializeField]
    //Behaviour[] close;

    [SerializeField]
    GameObject objectToDelete;

    // Start is called before the first frame update
    private void Start()
    {
        if(!isLocalPlayer)
        {
            /*for (int i=0; i<close.Length ; i++)
            {
                close[i].enabled = false;
                
            }*/
            objectToDelete.SetActive(false);
        }
        
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netId = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();
        LobbyManager.RegisterPlayer(netId, player);
    }
}
