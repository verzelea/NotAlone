using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : NetworkBehaviour
{
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Image validation;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private GameObject readyButtonCanvas;

    private void Start()
    {
        readyButtonCanvas = gameObject.transform.Find("PlayerCanvas/ReadyButton").gameObject;
        player = GetComponent<Player>();
        readyButton = gameObject.transform.Find("PlayerCanvas/ReadyButton/Button").GetComponent<Button>();
        validation = gameObject.transform.Find("PlayerCanvas/ReadyButton/Validation").GetComponent<Image>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        FunctionReadyButton();
    }

    private void Update()
    {
        if (player.GetLocation().HasValue)
        {
            readyButton.interactable = true;
        }
        else
        {
            readyButton.interactable = false;
        }
    }

    public void ShowButton(bool isMonsterOrNot)
    {
        if (player.data.IsMonster == isMonsterOrNot)
        {
            readyButtonCanvas.SetActive(true);
        }
    }

    public void ResetButton()
    {
        readyButton.gameObject.SetActive(true);
        validation.gameObject.SetActive(false);
        readyButtonCanvas.SetActive(false);
    }

    private void FunctionReadyButton()
    {
        readyButton.onClick.AddListener(ReadyCliked);
    }

    private void ReadyCliked()
    {
        player.data.IsReady = true;
        PlayerReadyClient(player.GetId(), true);
        readyButton.gameObject.SetActive(false);
        validation.gameObject.SetActive(true);
    }

    [Client]
    private void PlayerReadyClient(string id, bool ready)
    {
        if (isServer)
        {
            //envoi directement le message si c'est l'host qui envoi le message
            PlayerReadyRpc(id, ready);
        }
        else
        {
            PlayerReadyCommand(id, ready);
        }
    }

    //Le serveur demande à envoyer le message à tout le monde
    [Command]
    public void PlayerReadyCommand(string id, bool ready)
    {
        PlayerReadyRpc(id, ready);
    }

    //Le serveur envoi le message à tout le monde
    [ClientRpc]
    public void PlayerReadyRpc(string id, bool ready)
    {
        gameManager.SetPlayerReady(id, ready);
    }
}
