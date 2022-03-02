using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Networking;

[RequireComponent(typeof(TMP_Text))]
public class MotorChat : NetworkBehaviour
{
    public string player = "";

    public readonly ArrayList chat = new ArrayList();

    [SerializeField]
    private TMP_Text chatText = null;

    [SerializeField]
    private TMP_InputField inputField = null;
    
    void Start()
    {
        if(isLocalPlayer)
        {
            player = "player " + Random.Range(1, 100);
        }
    }

    public void Update()
    {
        
        if(Input.GetButtonDown("Submit") && !string.IsNullOrEmpty(inputField.text))
        {
            Debug.Log("start : " + player);
            Send();
        }
    }

    [Client]
    public void Send()
    {
        Debug.Log("send : " + player);
        string message = "\n" +player + " : " + inputField.text;
        if(!isServer)
        {
            SendChat(message);
        }
        else
        {
            UpdateTextFile(message);
        }
        inputField.text = string.Empty;
    }

    [Client]
    public void Send(string message)
    {
        if(!isServer)
        {
            SendChat(message);
        }
        else
        {
            UpdateTextFile(message);
        }
    }

    [Command]
    public void SetPlayer()
    {
        player = "player " + Random.Range(1, 100);
        //SetPlayerRpc();
    }

    [Command]
    public void SendChat(string message)
    {
        Debug.Log("command : " + player);
        UpdateTextFile(message);
    }

    [ClientRpc]
    public void UpdateTextFile(string message)
    {
        Debug.Log("update : " + player);
        chat.Add(message);
        chatText.text = string.Empty;
        foreach (string res in chat)
        {
            chatText.text += res;
        }
    }

    [ClientRpc]
    public void SetPlayerRpc()
    {
        if(isLocalPlayer)
        {
            player = "player " + Random.Range(1, 100);
        }
    }
}
