using UnityEngine;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    Button buttonTutorial;
    Button buttonLeave;

    // Start is called before the first frame update
    void Start()
    {
        buttonTutorial = gameObject.transform.Find("Menu/Tutorial").GetComponent<Button>();
        buttonLeave = gameObject.transform.Find("Menu/Leave").GetComponent<Button>();

        buttonTutorial.onClick.AddListener(OpenPdfTutorial);
        buttonLeave.onClick.AddListener(LeaveGame);
    }

    //Function for open pdf in browser
    private void OpenPdfTutorial()
    {
        Application.OpenURL("https://drive.google.com/file/d/1sgB9cSSScN-pf-P5PEA8ewhTRVrf-ftY/view?usp=sharing");
    }

    //Function for quit the application
    private void LeaveGame()
    {
        Application.Quit();
    }
}
