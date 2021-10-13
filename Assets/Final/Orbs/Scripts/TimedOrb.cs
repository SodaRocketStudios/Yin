using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedOrb : MonoBehaviour
{
    private WorldMaskMover maskMover;

    [SerializeField]
    private float moveDistance;

    [SerializeField]
    private float timeToTarget;

    [SerializeField]
    private float returnDelay;

    private bool isTriggered = false;

    private void Awake()
    {
        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        {
            maskMover.SetTarget(transform.position + Vector3.right*moveDistance, Vector3.zero, timeToTarget);
            isTriggered = true;

            StartCoroutine("ReturnToPlayer");
        }
    }

    IEnumerator ReturnToPlayer()
    {
        yield return new WaitForSeconds(returnDelay);

        maskMover.FollowPlayer();
    }
}
