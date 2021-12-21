using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject endScreen;

    [SerializeField]
    private YinController playerController;

    private Controls controls;

    private bool paused = false;
    private bool atMainMenu = false;

    private void Start()
    {
        controls = new Controls();

        controls.Enable();

        controls.Avatar.Quit.performed += (context) => Pause();
        MainMenu();
    }

    public void Play()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        playerController.EnableControls();
        atMainMenu = false;
    }

    public void Pause()
    {
        if(atMainMenu == false)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            playerController.DisableControls();
            endScreen.SetActive(false);
        }
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        endScreen.SetActive(false);
        playerController.DisableControls();
        atMainMenu = true;
    }

    public void SettingsMenu()
    {
        settingsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
