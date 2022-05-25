public class DontDestroyExceptDuplicate : DontDestroyOnLoad
{
    private static DontDestroyExceptDuplicate objectInstance;

    private new void Start()
    {
        base.Start();
        if (objectInstance == null)
        {
            objectInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
