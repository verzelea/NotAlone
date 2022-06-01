using System.Collections;
using TMPro;
using UnityEngine;

public class UpdateChat : MonoBehaviour
{
    private readonly ArrayList chat = new ArrayList();

    private TMP_Text chatText;

    private void Start()
    {
        chatText = gameObject.transform.Find("ChatCanvas/ChatUI/ScrollView/Viewport/ChatTextField").GetComponent<TMP_Text>();
    }

    //Add the message to the list of messages
    public void Add(string message)
    {
        if(chat.Count>=20)
        {
            chat.RemoveAt(0);
        }
        chat.Add(message);
        UpdateText();
    }

    //Update the chat text field
    public void UpdateText()
    {
        chatText.text = string.Empty;
        foreach (string res in chat)
        {
            chatText.text += res;
        }
    }

    //Reset the list and the text field
    public void ResetText()
    {
        chat.Clear();
        UpdateText();
    }
}