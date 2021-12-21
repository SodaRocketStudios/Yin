using UnityEngine;
using srs.Vector;

public class WorldMaskMover : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float defaultDampingTime;

    [SerializeField]
    private Vector3 defaultOffset;

    private FollowTarget playerTarget;

    private TargetFollower followerScript;

    private bool isFollowingPlayer = true;

    private void Awake()
    {
        followerScript = GetComponent<TargetFollower>();

        playerTarget = new FollowTarget();
        playerTarget.position = player.position.Flattened();
        playerTarget.dampingTime = defaultDampingTime;
        playerTarget.offset = defaultOffset;

        followerScript.Target = playerTarget;
    }

    private void Update()
    {
        if(isFollowingPlayer == true)
        {
            if(player.position.x > playerTarget.position.x)
            {
                playerTarget.position = player.position.Flattened();
                followerScript.Target = playerTarget;
            }
        }
        else if(player.position.x > followerScript.Target.position.x)
        {
            FollowPlayer();
        }
        
    }

    public void SetTarget(Vector3 position, Vector3 offset, float dampingTime = 0)
    {
        FollowTarget target = new FollowTarget();
        target.position = position.Flattened();
        target.offset = offset;
        target.dampingTime = dampingTime;

        isFollowingPlayer = false;

        followerScript.Target = target;
    }

    public void FollowPlayer()
    {
        isFollowingPlayer = true;

        playerTarget.position = player.position.Flattened();

        followerScript.Target = playerTarget;
        playerTarget.dampingTime = defaultDampingTime;
        playerTarget.offset = defaultOffset;
    }

    public void ResetToPlayer()
    {
        FollowTarget target = new FollowTarget();
        target.position = player.position.Flattened();
        target.offset = Vector3.zero;
        target.dampingTime = 0;

        isFollowingPlayer = false;

        followerScript.Target = target;
    }
}