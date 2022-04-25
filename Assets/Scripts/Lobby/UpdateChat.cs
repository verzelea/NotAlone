using System.Collections;
using TMPro;
using UnityEngine;

public class UpdateChat : MonoBehaviour
{
    public readonly ArrayList chat = new ArrayList();

    [SerializeField]
    private TMP_Text chatText;

    private void Start()
    {
        chatText = gameObject.transform.Find("LobbyCanvas/ChatUI/ScrollView/Viewport/ChatTextField").GetComponent<TMP_Text>();
    }

    //Ajouter le message à la liste des messages
    public void Add(string message)
    {
        if(chat.Count>=20)
        {
            chat.RemoveAt(0);
        }
        chat.Add(message);
        UpdateText();
    }

    //Met à jour le chat
    public void UpdateText()
    {
        chatText.text = string.Empty;
        foreach (string res in chat)
        {
            chatText.text += res;
        }
    }
}