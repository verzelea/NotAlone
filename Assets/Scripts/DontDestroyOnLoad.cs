using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    protected void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
