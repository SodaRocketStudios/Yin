using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderOrb : MonoBehaviour
{
    [SerializeField]
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
    }

    private void Update()
    {
        if(Time.time >= returnTime && hasBeenTriggered == true)
        {
            maskMover.SetTarget(returnPosition, Vector3.zero, timeToReturn);
        }

        // hasbeentriggered = false when returned.
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
