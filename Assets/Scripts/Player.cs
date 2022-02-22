using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 01;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        transform.Translate(new Vector2(1f, 0f) * moveSpeed*Time.deltaTime);
    }
}
