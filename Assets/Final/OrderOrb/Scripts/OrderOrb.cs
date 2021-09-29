using UnityEngine;

public class OrderOrb : MonoBehaviour
{
    private WorldMaskMover maskMover;

    [SerializeField]
    private float moveDistance;

    [SerializeField]
    private float timeToTarget;

    [SerializeField]
    private float returnDelay;

    private float returnTime;

    private bool hasBeenTriggered;

    private void Awake()
    {
        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void Update()
    {
        if(Time.time >= returnTime && hasBeenTriggered == true)
        {
            maskMover.FollowPlayer();
            hasBeenTriggered = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        {
            maskMover.SetTarget(transform.position + Vector3.left*moveDistance, Vector3.zero, timeToTarget);
            returnTime = Time.time + returnDelay;
            hasBeenTriggered = true;
        }
    }
}