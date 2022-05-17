using UnityEngine;

public class DestroyObjectIndestructible : MonoBehaviour
{
    // Au démarrage de la Scene Menu, il récupère les objets qui contiennent les scripts DontDestroyOnLoad, et les détruit.
    //Cela évite les doublons ou la configuration des objets qui ne sont pas détruit lors des changements de scène.
    void Start()
    {
        DontDestroyOnLoad[] test = FindObjectsOfType<DontDestroyOnLoad>();
        foreach (DontDestroyOnLoad a in test)
        {
            Destroy(a.gameObject);
        }
    }
}
