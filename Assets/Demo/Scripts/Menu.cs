using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;

    public void Play()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
