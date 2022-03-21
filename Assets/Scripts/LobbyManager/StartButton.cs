using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    public void AddStartButton()
    {
        startButton.gameObject.SetActive(true);
    }

    public void FunctionStartButton()
    {
        startButton.onClick.AddListener(StartGameCliked);
    }

    private void StartGameCliked()
    {
        NetworkManager.singleton.ServerChangeScene("Game");
    }
}
