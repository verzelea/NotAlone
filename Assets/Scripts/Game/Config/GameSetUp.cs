using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    private GameObject LobbyCanvas;
    private GameObject GameCanvas;
    private GameManager gameManager;
    private UpdateChat updateChat;
    private GameObject endScreen;

    private bool? keepIsGame;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        updateChat = GetComponent<UpdateChat>();
        LobbyCanvas = gameObject.transform.Find("LobbyCanvas").gameObject;
        GameCanvas = gameObject.transform.Find("GameCanvas").gameObject;
        endScreen = gameObject.transform.Find("EndCanvas").gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        if(keepIsGame == gameManager.GetIsGame())
        {
            return;
        }
        keepIsGame = gameManager.GetIsGame();
        if (!keepIsGame.Value)
        {
            SetupLobby();
        }
        else
        {
            SetupGame();
        }
    }

    public void SetupGame()
    {
        gameManager.SetSceneIsLoading(false);
        GameCanvas.SetActive(true);
        LobbyCanvas.SetActive(false);
        updateChat.ResetText();
        gameManager.StartGame();
    }

    public void SetupLobby()
    {
        gameManager.SetSceneIsLoading(false);
        endScreen.SetActive(false);
        GameCanvas.SetActive(false);
        LobbyCanvas.SetActive(true);
        updateChat.ResetText();
    }
}
