using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField]
    private FollowTarget target;
    public FollowTarget Target;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        MoveTowardTarget();
    }

    private void MoveTowardTarget()
    {
        Vector3 targetPosition = (target.position + target.offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, target.dampingTime);
    }
}

public struct FollowTarget
{
    public Vector3 position;
    public Vector3 offset;
    public float dampingTime;
}