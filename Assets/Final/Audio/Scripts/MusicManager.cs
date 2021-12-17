using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> tracks;

    [SerializeField]
    private AudioMixerGroup orderOutput;

    [SerializeField]
    private AudioMixerGroup chaosOutput;

    

    private AudioSource source;

    private int previousTrack = -1;

    private bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        PlaySong();
    }

    private void PlaySong()
    {
        int i;
        do
        {
            i = Random.Range(0, tracks.Count);
        }
        while(previousTrack == i);

        previousTrack = i;

        source.clip = tracks[i];
        source.Play();
    }

    private void Update()
    {
        if(source.isPlaying == false && waiting == false)
        {
            StartCoroutine("WaitForNextSong");
            waiting = true;
        }
    }

    IEnumerator WaitForNextSong()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        PlaySong();
        waiting = false;
    }

    public void SwitchToChaosOutput()
    {
        source.outputAudioMixerGroup = chaosOutput;
    }

    public void SwitchToOrderOutput()
    {
        source.outputAudioMixerGroup = orderOutput;
    }
}
