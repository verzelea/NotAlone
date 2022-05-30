using UnityEngine;

public class GameSetUp : MonoBehaviour
{
    [SerializeField]
    private GameObject LobbyCanvas;
    [SerializeField]
    private GameObject GameCanvas;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private UpdateChat updateChat;
    [SerializeField]
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
        if(keepIsGame == gameManager.isGame)
        {
            return;
        }
        keepIsGame = gameManager.isGame;
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
        gameManager.sceneIsLoading = false;
        GameCanvas.SetActive(true);
        LobbyCanvas.SetActive(false);
        updateChat.ResetText();
        gameManager.StartGame();
    }

    public void SetupLobby()
    {
        gameManager.sceneIsLoading = false;
        endScreen.SetActive(false);
        GameCanvas.SetActive(false);
        LobbyCanvas.SetActive(true);
        updateChat.ResetText();
    }



}
