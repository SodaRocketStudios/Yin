using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Checkpoint checkpoint;

    private void Load()
    {
        // Load the last checkpoint.
        // checkpoint = 

        // Set the player position
        player.transform.position = checkpoint.transform.position;
    }

    private void Save()
    {

    }
}