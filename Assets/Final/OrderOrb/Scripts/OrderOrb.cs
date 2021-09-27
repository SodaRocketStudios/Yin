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

    [SerializeField]
    private float timeToReturn;

    private Vector3 returnPosition;
    private float returnTime;

    private bool hasBeenTriggered;

    private void Awake()
    {
        returnPosition = transform.position;

        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void Update()
    {
        if(Time.time >= returnTime && hasBeenTriggered == true)
        {
            maskMover.SetTarget(returnPosition, Vector3.zero, timeToReturn);
        }

        // when returned, follow the player again.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        {
            maskMover.SetTarget(returnPosition + Vector3.left*moveDistance, Vector3.zero, timeToTarget);
            returnTime = Time.time + returnDelay;
            hasBeenTriggered = true;
        }
    }
}
