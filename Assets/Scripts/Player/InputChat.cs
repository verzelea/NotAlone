using Mirror;
using TMPro;
using UnityEngine;

public class InputChat : NetworkBehaviour
{
    private UpdateChat update = null;

    [SerializeField]
    private TMP_InputField inputField = null;

    void Start()
    {
        update = GameObject.Find("GameManager").GetComponent<UpdateChat>();
        
        //update = GameObject.Find(manager).GetComponent<UpdateChat>();
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
        Player player = GetComponent<Player>();
        string message = "\n" + player.GetPlayer() + " : " + inputField.text;
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
}
