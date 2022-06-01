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
    }

    //Check if the button Enter is use and the text field is not empty
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) 
        && !string.IsNullOrEmpty(inputField.text))
        {
            Send();
        }
    }

    //Client create a message and send it
    [Client]
    public void Send()
    {
        Player player = GetComponent<Player>();
        string message = "\n" + player.GetPlayer() + " : " + inputField.text;
        if(isServer)
        {
            //Send directly the message if it's the host who send it
            UpdateTextFile(message);
        }
        else
        {
            SendChat(message);
        }
        inputField.text = string.Empty;
    }

    //Serveur send the request to all clients
    [Command]
    public void SendChat(string message)
    {
        UpdateTextFile(message);
    }

    //All clients add the new message
    [ClientRpc]
    public void UpdateTextFile(string response)
    {
        update.Add(response);
    }
}
