using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBehaviour : MonoBehaviour
{
    [SerializeField] private bool isChatUpdated = false;

    [SerializeField] private Queue<string> chat = new Queue<string>();

    [SerializeField] private TMP_Text chatText = null;
    [SerializeField] private TMP_InputField inputField = null;

    public void Update()
    {
        if(Input.GetButtonDown("Submit") && !string.IsNullOrEmpty(inputField.text))
        {
            SendChat(inputField.text);
            inputField.text = string.Empty;
            isChatUpdated = true;
        }

        if(isChatUpdated)
        {
            UpdateTextFile();
        }
    }

    private void SendChat(string message){
        if(chat.Count>=20)
        {
            chat.Dequeue();
        }
        chat.Enqueue(message);
    }

    private void UpdateTextFile(){
        chatText.text = string.Empty;
        foreach (string message in chat)
        {
            chatText.text+="User1" + " says: " + message + "\n";
        }
        isChatUpdated = false;
    }
}
