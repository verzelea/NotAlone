using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] close;

    [SerializeField]
    Behaviour[] open;

    [SerializeField]
    GameObject objectToDelete;

    // Start is called before the first frame update
    private void Start()
    {
        if(!isLocalPlayer)
        {
            for (int i=0; i<close.Length ; i++)
            {
                close[i].enabled = false;
                
            }
            objectToDelete.SetActive(false);
        }

        if(isLocalPlayer)
        {
            for (int i=0; i<open.Length ; i++)
            {
                open[i].enabled = true;
            }
        }
        
    }
}
