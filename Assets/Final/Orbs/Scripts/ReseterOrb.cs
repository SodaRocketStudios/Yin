using UnityEngine;

public class ReseterOrb : MonoBehaviour
{
    private WorldMaskMover maskMover;

    private bool triggerEnabled = false;

    private void Awake()
    {
        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(triggerEnabled == true && other.CompareTag("Player") == true)
        {
            maskMover.FollowPlayer();
        }
        else if(other.CompareTag("Mask") == true)
        {
            triggerEnabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Mask") == true)
        {
            triggerEnabled = false;
        }
    }
}