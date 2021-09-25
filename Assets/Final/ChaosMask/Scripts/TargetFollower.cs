using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public Transform Target {set {target = value;}}

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float dampingTime;

    private Vector3 velocity = Vector3.zero;

    private void Update()
    {
        MoveTowardTarget();   
    }

    private void MoveTowardTarget()
    {
        Vector3 targetPosition = (target.position + offset);
        targetPosition.y = 0;
        if(transform.position.x < targetPosition.x)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampingTime);
        }
    }
}
