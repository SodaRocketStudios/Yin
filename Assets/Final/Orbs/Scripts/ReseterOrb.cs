using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReseterOrb : MonoBehaviour
{
    private WorldMaskMover maskMover;

    private void Awake()
    {
        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        maskMover.FollowPlayer();
    }
}
