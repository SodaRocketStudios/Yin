using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    private float minPitch = 1;

    [SerializeField]
    private float maxPitch = 1;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponentInParent<AudioSource>();
    }

    public void Step()
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }
}
