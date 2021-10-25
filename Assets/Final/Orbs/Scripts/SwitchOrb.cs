using UnityEngine;

public class SwitchOrb : MonoBehaviour
{
    private WorldMaskMover maskMover;

    [SerializeField]
    private float moveDistance;

    [SerializeField]
    private float timeToTarget;

    private bool triggerEnabled = true;

    private void Awake()
    {
        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(triggerEnabled == true && other.CompareTag("Player") == true)
        {
            maskMover.SetTarget(transform.position + Vector3.right*moveDistance, Vector3.zero, timeToTarget);
        }
        else if(other.CompareTag("Mask") == true)
        {
            triggerEnabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Mask") == true)
        {
            triggerEnabled = true;
        }
    }
}