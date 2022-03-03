using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Networking;

[RequireComponent(typeof(UpdateChat), typeof(TMP_InputField), typeof(PlayerData))]
public class InputChat : NetworkBehaviour
{
    private UpdateChat update = null;

    [SerializeField]
    public PlayerData data = null;

    [SerializeField]
    private TMP_InputField inputField = null;
    
    void Start()
    {
        update = GameObject.Find("LobbyCanvas").GetComponent<UpdateChat>();
        
    }

    //On vérifie si le client appuie sur entrée et que le champ de saisie contient du texte
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) 
        && !string.IsNullOrEmpty(inputField.text))
        {
            Send();
        }
    }

    //Le client crée le message et l'envoi au serveur
    [Client]
    public void Send()
    {
        string message = "\n" + data.GetPlayer() + " : " + inputField.text;
        if(!isServer)
        {
            SendChat(message);
        }
        else
        {
            //envoi directement le message si c'est l'host qui envoi le message
            UpdateTextFile(message);
        }
        inputField.text = string.Empty;
    }

    //Le serveur demande à envoyer le message à tout le monde
    [Command]
    public void SendChat(string message)
    {
        UpdateTextFile(message);
    }

    //Le serveur envoi le message à tout le monde
    [ClientRpc]
    public void UpdateTextFile(string response)
    {
        update.Add(response);
    }

    /*
    [Command]
    public void SetPlayer()
    {
        player = "player " + Random.Range(1, 100);
        //SetPlayerRpc();
    }

    [ClientRpc]
    public void SetPlayerRpc()
    {
        if(isLocalPlayer)
        {
            player = "player " + Random.Range(1, 100);
        }
    }*/
}
