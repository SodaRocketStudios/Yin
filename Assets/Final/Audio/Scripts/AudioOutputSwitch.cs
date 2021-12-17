using UnityEngine;

public class AudioOutputSwitch : MonoBehaviour
{
    [SerializeField]
    private MusicManager musicManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Mask"))
        {
            musicManager.SwitchToChaosOutput();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Mask"))
        {
            musicManager.SwitchToOrderOutput();
        }
    }
}
