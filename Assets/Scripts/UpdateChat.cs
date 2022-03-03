using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Networking;

[RequireComponent(typeof(TMP_Text))]
public class UpdateChat : NetworkBehaviour
{
    public readonly ArrayList chat = new ArrayList();

    [SerializeField]
    private TMP_Text chatText = null;

    //Ajouter le message à la liste des messages
    public void Add(string message)
    {
        Debug.Log("add");
        chat.Add(message);
        UpdateText();
    }

    //Met à jour le chat
    public void UpdateText()
    {
        Debug.Log("update");
        chatText.text = string.Empty;
        foreach (string res in chat)
        {
            chatText.text += res;
        }
    }
}
