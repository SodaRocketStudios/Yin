using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] clips;

    [SerializeField]
    private float minPitch = 1;

    [SerializeField]
    private float maxPitch = 1;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private ParticleSystem runParticles;

    

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }

    public void Step()
    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
        runParticles.Play();
    }
}
