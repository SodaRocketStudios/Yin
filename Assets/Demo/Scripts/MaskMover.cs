using UnityEngine;

public class MaskMover : MonoBehaviour
{
    [SerializeField]
    private SpriteMask mask;

    [SerializeField]
    private float maskLagDistance;

    void Update()
    {
        if(mask.transform.position.x < transform.position.x - maskLagDistance)
        {
            mask.transform.position = (transform.position.x - maskLagDistance)*Vector2.right;
        }
    }
}
