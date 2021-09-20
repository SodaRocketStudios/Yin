using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool hasBeenActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        {
            if(hasBeenActivated == false)
            {
                // Set this checkpoint as the current checkpoint
                
                hasBeenActivated = true;
            }
        }
    }
}
