using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    
    [SerializeField]
    private GameObject settingsMenu;

    public void Play()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void Pause()
    {

    }

    public void Quit()
    {

    }
}
