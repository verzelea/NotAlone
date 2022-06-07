using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : NetworkBehaviour
{
    private Player player;
    private bool tmpIsready = false;
    private Transform locationCanvas;

    private void Start()
    {
        locationCanvas = gameObject.transform.Find("PlayerCanvas/LocationUI");
        player = GetComponent<Player>();
    }

    //If a location of a player is empty, all the location buttons is grey and non-interactive
    private void Update()
    {
        if (tmpIsready == player.GetIsReady())
        {
            return;
        }
        tmpIsready = player.GetIsReady();

        int children = locationCanvas.childCount;
        for (int i = 0; i < children; ++i)
        {
            Button button = locationCanvas.GetChild(i).gameObject.GetComponent<Button>();
            button.interactable = !tmpIsready;
        }
    }

    //Set the function for each button Location
    public void SetUpLocations(Transform objectButton)
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        int children = objectButton.childCount;
        for (int i = 0; i < children; ++i)
        {
            var child = objectButton.GetChild(i).gameObject;

            Image border = child.transform.Find("Image").GetComponent<Image>();
            border.enabled = false;

            Button button = child.GetComponent<Button>();
            switch (child.name)
            {
                case "Antre":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Antre));
                    break;
                case "Jungle":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Jungle));
                    break;
                case "Riviere":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Riviere));
                    break;
                case "Plage":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Plage));
                    break;
                case "Rover":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Rover));
                    break;
                case "Marais":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Marais));
                    break;
                case "Abri":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Abri));
                    break;
                case "Epave":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Epave));
                    break;
                case "Source":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Source));
                    break;
                case "Artefact":
                    button.onClick.AddListener(() => OnClickLocation(LocationEnum.Artefact));
                    break;
            }
        }
    }

    //The function of the button location
    private void OnClickLocation(LocationEnum location)
    {
        player.SetLocation(location);
    }

}
