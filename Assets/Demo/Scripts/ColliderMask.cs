using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ColliderMask : MonoBehaviour
{
    [SerializeField]
    private LayerMask chaosMask;

    [SerializeField]
    private float checksPerSecond = 10;

    [SerializeField]
    private float worldCheckRadius = 0.1f;

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioMixerGroup orderGroup;

    [SerializeField]
    private AudioMixerGroup chaosGroup;

    private void Start()
    {
        InvokeRepeating("CheckWorld", 0, 1.0f/checksPerSecond);
    }

    private void CheckWorld()
    {
        Collider2D overlap = Physics2D.OverlapCircle(transform.position, worldCheckRadius, chaosMask);

        // If the avatar is in the order world
        if(overlap == null)
        {
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("Order Player"));
            source.outputAudioMixerGroup = orderGroup;
        }
        else
        {
            gameObject.SetLayerRecursively(LayerMask.NameToLayer("Chaos Player"));
            source.outputAudioMixerGroup = chaosGroup;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, worldCheckRadius);
    }
}
