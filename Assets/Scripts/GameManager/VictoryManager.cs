using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    [SerializeField]
    Slider sliderPlayer;

    [SerializeField]
    Slider sliderMonster;

    [SerializeField]
    GameObject endScreen;

    [SerializeField]
    Image tokenSurvivor;

    [SerializeField]
    Image tokenMonster;

    [SerializeField]
    TMP_Text descriptionEnd;

    [SerializeField]
    Button buttonMenu;

    //delete
    private bool check = false;

    public float pointSurvivant = 0.1f;

    public float pointMonstre = 0.1f;

    private void Start()
    {
        tokenSurvivor.enabled = false;
        tokenMonster.enabled = false;

        GameManager manager = GetComponent<GameManager>();
        pointSurvivant = (float)1 / (12 + manager.CountPlayer());
        pointMonstre = (float)1 / (6 + manager.CountPlayer());
    }

    //delete
    private void Update()
    {
        if (!check)
        {
            check = AddPointMonstre(1);
        }
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
            endScreen.SetActive(true);
            return true;
        }
        return false;
    }

    public void AddReturnButton()
    {
        buttonMenu.gameObject.SetActive(true);
        FunctionReturnButton();
    }

    private void FunctionReturnButton()
    {
        buttonMenu.onClick.AddListener(ReturnLobbyCliked);
    }

    private void ReturnLobbyCliked()
    {
        NetworkManager.singleton.ServerChangeScene("Lobby");
    }
}
