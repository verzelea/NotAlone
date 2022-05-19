using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Scene? scene = null;

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
        var tmp = SceneManager.GetActiveScene();
        if (tmp != scene)
        {
            scene = tmp;
            if (scene?.name == "Lobby")
            {
                SetupLobby();
            }
            else if (scene?.name == "Game")
            {
                SetupGame();
            }
        }
        return;
    }

    public void SetupGame()
    {
        gameManager.sceneIsLoading = false;
        GameCanvas.SetActive(true);
        LobbyCanvas.SetActive(false);
        updateChat.ResetText();
        gameManager.isGame = true;
        gameManager.StartGame();
    }

    public void SetupLobby()
    {
        gameManager.sceneIsLoading = false;
        endScreen.SetActive(false);
        GameCanvas.SetActive(false);
        LobbyCanvas.SetActive(true);
        gameManager.isGame = false;
        updateChat.ResetText();
    }



}
