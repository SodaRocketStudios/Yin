using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    public void Clear()
    {
        if(File.Exists($"{Application.persistentDataPath}/SaveData.dat"))
        {
            File.Delete($"{Application.persistentDataPath}/SaveData.dat");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
