using UnityEngine;

public class SwitchOrb : MonoBehaviour
{
    private WorldMaskMover maskMover;

    [SerializeField]
    private float orderTarget;

    [SerializeField]
    private float chaosTarget;

    [SerializeField]
    private float timeToTarget;

    private bool useChaosTarget = true;

    private void Awake()
    {
        maskMover = FindObjectOfType<WorldMaskMover>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        {
            float moveDistance = useChaosTarget?chaosTarget:orderTarget;
            maskMover.SetTarget(transform.position + Vector3.right*moveDistance, Vector3.zero, timeToTarget);
            
            useChaosTarget = !useChaosTarget;
        }
    }
}