using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    private Slider sliderPlayer;
    private Slider sliderMonster;
    private GameObject endCanvas;
    private Image tokenSurvivor;
    private Image tokenMonster;
    private TMP_Text descriptionEnd;
    private Button buttonMenu;
    private GameManager gameManager;
    private ChangeStatutGame changeStatutGame;

    private float pointSurvivant = 0.1f;
    private float pointMonstre = 0.1f;

    private void Start()
    {
        sliderPlayer = gameObject.transform.Find("GameCanvas/TaskBar/SliderPlayer").GetComponent<Slider>();
        sliderMonster = gameObject.transform.Find("GameCanvas/TaskBar/SliderMonster").GetComponent<Slider>();
        endCanvas = gameObject.transform.Find("EndCanvas").gameObject;
        tokenSurvivor = gameObject.transform.Find("EndCanvas/EndScreen/TokenSurvivor").GetComponent<Image>();
        tokenMonster = gameObject.transform.Find("EndCanvas/EndScreen/TokenMonster").GetComponent<Image>();
        descriptionEnd = gameObject.transform.Find("EndCanvas/EndScreen/Description").GetComponent<TMP_Text>();
        buttonMenu = gameObject.transform.Find("EndCanvas/EndScreen/ReturnButton").GetComponent<Button>();

        gameManager = GetComponent<GameManager>();
        tokenSurvivor.enabled = false;
        tokenMonster.enabled = false;

        pointSurvivant = (float)1 / (12 + gameManager.CountPlayer());
        pointMonstre = (float)1 / (6 + gameManager.CountPlayer());
    }

    //Add param n points for the survivors
    public bool AddPointPlayer(int n)
    {
        sliderPlayer.value += pointSurvivant * n;
        return CheckVictory();
    }

    //Add param n points for the monster
    public bool AddPointMonstre(int n)
    {
        sliderMonster.value += pointSurvivant * n;
        return CheckVictory();
    }

    //Check if the survivors or the monster reached the goal of victory
    private bool CheckVictory()
    {
        if(sliderMonster.value >= 1 || sliderPlayer.value >= 1)
        {
            if (sliderMonster.value >= 1)
            {
                descriptionEnd.text = "Monster win";
                tokenMonster.enabled = true;
            }
            else 
            {
                descriptionEnd.text = "Survivors win";
                tokenSurvivor.enabled = true;
            }
            endCanvas.SetActive(true);
            return true;
        }
        return false;
    }

    //Add the function in the return button
    public void AddReturnButton()
    {
        gameManager.GetPlayers().TryGetValue(gameManager.GetLocalPlayer(), out Player local);
        changeStatutGame = local.gameObject.GetComponent<ChangeStatutGame>();

        buttonMenu.onClick.AddListener(ReturnLobbyCliked);
        buttonMenu.gameObject.SetActive(true);
    }

    //Stop the game and return clients to lobby
    private void ReturnLobbyCliked()
    {
        var players = gameManager.GetPlayers();
        foreach (Player player in players.Values) {
            changeStatutGame.StopGame(player);
        }
    }

    //Reset this manager for the next game
    public void ResetManager()
    {
        sliderPlayer.value = 0;
        sliderMonster.value = 0;
        tokenSurvivor.enabled = false;
        tokenMonster.enabled = false;
    }
}
