using Mirror;
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
                    button.onClick.AddListener(OnClickAntre);
                    break;
            }
        }
    }

    private void OnClickAntre()
    {
        player.SetLocation(LocationEnum.Antre);
    }
}
