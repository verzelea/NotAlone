using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IpAddress : MonoBehaviour
{
    private const string NotFound = "IpAddress is not found";

    private Button buttonCopyIp;
    private TMP_Text textField;
    private GameObject ipGroup;
    private GameManager gameManager;

    private async void Start()
    {
        await Task.Delay(25);
        gameManager = GetComponent<GameManager>();
        if (!gameManager.GetIsServer())
        {
            return;
        }
        ipGroup = gameObject.transform.Find("LobbyCanvas/Ip").gameObject;
        textField = ipGroup.transform.Find("Text/IP_text").GetComponent<TMP_Text>();
        buttonCopyIp = ipGroup.transform.Find("Button").GetComponent<Button>();

        ipGroup.SetActive(true);
        string ip = IpStatic.Get_IPv6_Local_IP();
        if(string.IsNullOrEmpty(ip))
        {
            textField.text = NotFound;
        }
        else
        {
            textField.text = ip;
        }
        buttonCopyIp.onClick.AddListener(CopyButton);
    }

    private void CopyButton()
    {
        if (textField.text == NotFound)
        {
            IpStatic.CopyToClipboard(null);
        }
        else
        {
            textField.text.CopyToClipboard();
        }
    }
}