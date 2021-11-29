using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    
    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private YinController playerController;

    public void Play()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        playerController.EnableControls();
    }

    public void Pause()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        playerController.DisableControls();
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        playerController.DisableControls();
    }

    public void Quit()
    {

    }
}
