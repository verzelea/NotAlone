using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    [SerializeField]
    private Slider sliderPlayer;

    [SerializeField]
    private Slider sliderMonster;

    [SerializeField]
    private GameObject endCanvas;

    [SerializeField]
    private Image tokenSurvivor;

    [SerializeField]
    private Image tokenMonster;

    [SerializeField]
    private TMP_Text descriptionEnd;

    [SerializeField]
    private Button buttonMenu;

    public float pointSurvivant = 0.1f;

    public float pointMonstre = 0.1f;

    private void Start()
    {
        sliderPlayer = gameObject.transform.Find("GameCanvas/TaskBar/SliderPlayer").GetComponent<Slider>();
        sliderMonster = gameObject.transform.Find("GameCanvas/TaskBar/SliderMonster").GetComponent<Slider>();
        endCanvas = gameObject.transform.Find("EndCanvas").gameObject;
        tokenSurvivor = gameObject.transform.Find("EndCanvas/EndScreen/TokenSurvivor").GetComponent<Image>();
        tokenMonster = gameObject.transform.Find("EndCanvas/EndScreen/TokenMonster").GetComponent<Image>();
        descriptionEnd = gameObject.transform.Find("EndCanvas/EndScreen/Description").GetComponent<TMP_Text>();
        buttonMenu = gameObject.transform.Find("EndCanvas/EndScreen/ReturnButton").GetComponent<Button>();

        tokenSurvivor.enabled = false;
        tokenMonster.enabled = false;

        NewGameManager manager = GetComponent<NewGameManager>();
        pointSurvivant = (float)1 / (12 + manager.CountPlayer());
        pointMonstre = (float)1 / (6 + manager.CountPlayer());
    }

    //Ajoute n points pour les survivants
    public bool AddPointPlayer(int n)
    {
        sliderPlayer.value += pointSurvivant * n;
        return CheckVictory();
    }

    //Ajoute n points pour le monstre
    public bool AddPointMonstre(int n)
    {
        sliderMonster.value += pointSurvivant * n;
        return CheckVictory();
    }

    //Verifie si les survivants ou le monstre ont atteint la condition de victoire
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

    //Ajoute le bouton de retour au Lobby, et y associe la méthode de retour
    public void AddReturnButton()
    {
        buttonMenu.onClick.AddListener(ReturnLobbyCliked);
        buttonMenu.gameObject.SetActive(true);
    }

    private void ReturnLobbyCliked()
    {
        NetworkManager.singleton.ServerChangeScene("Lobby");
        GetComponent<NewGameManager>().SetupLobby();
    }
}
