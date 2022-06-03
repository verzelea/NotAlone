using UnityEngine;
using UnityEngine.UI;

public class LocationBorderValidation : MonoBehaviour
{
    private Player player;
    private LocationEnum? locationtmp = null; 
    private Transform locationCanvas;

    private void Start()
    {
        locationCanvas = gameObject.transform.Find("PlayerCanvas/LocationUI");
        player = GetComponent<Player>();
    }

    //If a location of a player is empty, all the location buttons is grey and non-interactive
    private void Update()
    {
        if (locationtmp == player.GetLocation())
        {
            return;
        }
        locationtmp = player.GetLocation();
        

        int children = locationCanvas.childCount;
        for (int i = 0; i < children; ++i)
        {
            Image border = locationCanvas.GetChild(i).Find("Image").GetComponent<Image>();
            border.enabled = locationtmp.HasValue &&
                locationCanvas.GetChild(i).gameObject.name == EnumStatic.GetEnumDescription(locationtmp.Value)
                ? true : false;
        }
    }
}
