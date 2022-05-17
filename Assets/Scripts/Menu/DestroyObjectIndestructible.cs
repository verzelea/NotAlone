using UnityEngine;

public class DestroyObjectIndestructible : MonoBehaviour
{
    // Au d�marrage de la Scene Menu, il r�cup�re les objets qui contiennent les scripts DontDestroyOnLoad, et les d�truit.
    //Cela �vite les doublons ou la configuration des objets qui ne sont pas d�truit lors des changements de sc�ne.
    void Start()
    {
        DontDestroyOnLoad[] test = FindObjectsOfType<DontDestroyOnLoad>();
        foreach (DontDestroyOnLoad a in test)
        {
            Destroy(a.gameObject);
        }
    }
}
