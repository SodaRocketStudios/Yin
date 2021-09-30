using UnityEngine;

public class SwitchOrb : MonoBehaviour
{
    private WorldMaskMover maskMover;

    [SerializeField]
    private float moveDistance;

    [SerializeField]
    private float timeToTarget;

    private bool hasBeenTriggered;

    private void Awake()
    {
        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        {
            maskMover.SetTarget(transform.position + Vector3.right*moveDistance, Vector3.zero, timeToTarget);
            hasBeenTriggered = true;
        }
    }
}