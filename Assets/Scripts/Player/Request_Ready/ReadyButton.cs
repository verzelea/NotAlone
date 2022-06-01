using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class ReadyButton : NetworkBehaviour
{
    private Button readyButton;
    private Player player;
    private Image validation;
    private GameManager gameManager;
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

    //If a location of a player is empty, the ready button is grey and non-interactive
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

    //Show the button for survivors if param is false, else, show the button for the monster
    public void ShowButton(bool isMonster)
    {
        if (player.GetIsMonster() == isMonster)
        {
            readyButtonCanvas.SetActive(true);
        }
    }

    //Reset all the ready button
    public void ResetButton()
    {
        readyButton.gameObject.SetActive(true);
        validation.gameObject.SetActive(false);
        readyButtonCanvas.SetActive(false);
    }

    //Set the function for the ready button
    private void FunctionReadyButton()
    {
        readyButton.onClick.AddListener(ReadyCliked);
    }

    //Function for the ready button
    private void ReadyCliked()
    {
        player.SetIsReady(true);
        PlayerReadyClient(player.GetId(), true);
        readyButton.gameObject.SetActive(false);
        validation.gameObject.SetActive(true);
    }

    //Tell to the player, with the param id, that his attribute IsReady is equal to param ready
    [Client]
    private void PlayerReadyClient(string id, bool ready)
    {
        if (isServer)
        {
            //Send directly the message if it's the host who send it
            PlayerReadyRpc(id, ready);
        }
        else
        {
            PlayerReadyCommand(id, ready);
        }
    }

    //The server send the request for all the clients
    [Command]
    public void PlayerReadyCommand(string id, bool ready)
    {
        PlayerReadyRpc(id, ready);
    }

    //Each clients set the attribute IsReady of his player
    [ClientRpc]
    public void PlayerReadyRpc(string id, bool ready)
    {
        gameManager.SetPlayerReady(id, ready);
    }
}
