using Mirror;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LocationManager : NetworkBehaviour
{
    private Player player;
    
    public void SetUpLocations(GameObject manager, Transform objectButton)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        player = GetComponent<Player>();
        int children = objectButton.childCount;
        for (int i = 0; i < children; ++i)
        {
            var child = objectButton.GetChild(i).gameObject;
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

    private void OnClickLocation(LocationEnum location)
    {
        player.SetLocation(location);
    }

}
