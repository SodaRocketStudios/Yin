using UnityEngine;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField]
    private AudioMixer mixer;

    public void SetVolume(float vol)
    {
        float volume = 20*Mathf.Log10(vol);
        mixer.SetFloat("MasterVolume", volume);
    }
}
