using UnityEngine;

public class PolyColliderMask : MonoBehaviour
{
    private PolygonCollider2D polyCollider;

    private Vector2[] defaultPoints;
    private Vector2[] worldPoints;
    private int[] orderedPointIndices;

    private float angle;

    private bool isBetweenWorlds;
    private bool isInHomeWorld;
    private bool isDefault;

    [SerializeField]
    private BoxCollider2D mask;

    [SerializeField]
    private bool isChaosElement;

    private void Awake()
    {
        polyCollider = GetComponent<PolygonCollider2D>();
        defaultPoints = new Vector2[polyCollider.GetTotalPointCount()];
        polyCollider.points.CopyTo(defaultPoints, 0);

        orderedPointIndices = new int[]{-1, -1, -1, -1};
        worldPoints = new Vector2[defaultPoints.Length];

        // Get the slope angle of the collider.
        angle = Mathf.Deg2Rad*(transform.rotation.eulerAngles.z + 90);

        for(int i = 0; i < defaultPoints.Length; i++)
        {
            worldPoints[i] = transform.TransformPoint(defaultPoints[i]);
        }

        OrganizePoints();
    }

    private void SetPoints()
    {
        float maskEdge = mask.bounds.center.x + mask.bounds.extents.x;

        Vector2[] points = new Vector2[defaultPoints.Length];
        defaultPoints.CopyTo(points, 0);

        for(int i = 0; i < 2; i++)
        {
            Vector2 worldPoint = worldPoints[orderedPointIndices[i]];
            float dx = (worldPoint.x - maskEdge);
            float dy = dx*Mathf.Tan(angle);
            Vector2 displacement = Vector2.left*dx + Vector2.down*dy;

            float newX = (worldPoint + displacement).x;

            // If beyond the default shape, don't add displacement.
            bool outOfRange = newX > worldPoint.x && isChaosElement == true;
            outOfRange |= newX < worldPoint.x && isChaosElement == false;

            if(outOfRange == true)
            {
                dx = 0;
                dy = 0;
                displacement = Vector2.zero;
            }

            Vector2 newPoint = worldPoint + displacement;

            outOfRange = newX < worldPoints[orderedPointIndices[i+2]].x && isChaosElement == true;
            outOfRange |= newX > worldPoints[orderedPointIndices[i+2]].x && isChaosElement == false;

            if(outOfRange == true)
            {
                newPoint = worldPoints[orderedPointIndices[i+2]];
            }

            points[orderedPointIndices[i]] = transform.InverseTransformPoint(newPoint);
            
            Debug.DrawLine(worldPoint, worldPoint + displacement, Color.yellow);
            Debug.DrawLine(worldPoint, worldPoint + (Vector2.left*dx), Color.red);
            Debug.DrawLine(worldPoint + Vector2.left*dx, worldPoint + Vector2.down*dy + Vector2.left*dx, Color.blue);
        }

        polyCollider.points = points;
    }

    // Determines if the collider is in the home world, between worlds, or is in the opposite world.
    private void CheckWorld()
    {
        bool lastPointInHomeWorld = false;
        bool pointIsInHomeWorld = false;

        isInHomeWorld = true;
        isBetweenWorlds = false;
        

        for(int i = 0; i < defaultPoints.Length; i++)
        {
            // true if the point is in the mask collider and it is marked as chaos or if it is outside the mask and marked as not chaos
            pointIsInHomeWorld = mask.bounds.Contains(transform.TransformPoint(defaultPoints[i])) == isChaosElement;

            // If the point is in the home world.
            if(pointIsInHomeWorld == true)
            {
                // if the last point was not in the home world
                if(lastPointInHomeWorld == false && i > 0)
                {
                    isBetweenWorlds = true;
                    isInHomeWorld = false;
                    return;
                }

                lastPointInHomeWorld = true;
            }
            else
            {
                isInHomeWorld = false;

                // if the last point was in the home world
                if(lastPointInHomeWorld == true && i > 0)
                {
                    isBetweenWorlds = true;
                    return;
                }

                lastPointInHomeWorld = false;
            }
        }
    }

    private void OrganizePoints()
    {
        // Order the points based on which is furthes to the right.
        for(int i = 0; i < orderedPointIndices.Length; i++)
        {
            if(i == 0)
            {
                orderedPointIndices[0] = i;
            }
            else
            {
                Vector2 worldPoint = worldPoints[i];

                for (int j = i; j > 0; j--)
                {
                    if(worldPoint.x > worldPoints[orderedPointIndices[j-1]].x)
                    {
                        orderedPointIndices[j] = orderedPointIndices[j-1];
                        orderedPointIndices[j-1] = i;
                    }
                    else
                    {
                        orderedPointIndices[j] = i;
                        break;
                    }
                }
            }
        }

        // Make sure that the top right point is index 0 and the bottom right is index 1.
        if(worldPoints[orderedPointIndices[1]].y > worldPoints[orderedPointIndices[0]].y)
        {
            int temp = orderedPointIndices[0];
            orderedPointIndices[0] = orderedPointIndices[1];
            orderedPointIndices[1] = temp;
        }
        // Make sure that the top left point is index 2 and the bottom left is index 3.
        if(worldPoints[orderedPointIndices[3]].y > worldPoints[orderedPointIndices[2]].y)
        {
            int temp = orderedPointIndices[2];
            orderedPointIndices[2] = orderedPointIndices[3];
            orderedPointIndices[3] = temp;
        }

        // if not a chaos element, switch left and right.
        if(isChaosElement == false)
        {
            for(int i = 0; i < 2; i++)
            {
                int temp = orderedPointIndices[i];
                orderedPointIndices[i] = orderedPointIndices[i+2];
                orderedPointIndices[i+2] = temp;
            }
            
        }
    }

    private void LateUpdate()
    {
        CheckWorld();
        if(isBetweenWorlds == true)
        {
            polyCollider.enabled = true;
            isDefault = false;
            SetPoints();
        }
        else
        {
            if(isDefault == false)
            {
                Vector2[] newPoints = new Vector2[defaultPoints.Length];
                for(int i = 0; i < defaultPoints.Length; i++)
                {
                    newPoints[i] = defaultPoints[i];
                }
                isDefault = true;
                polyCollider.points = newPoints;
            }

            if(isInHomeWorld == false)
            {
                polyCollider.enabled = false;
            }
            else
            {
                polyCollider.enabled = true;
            }
        }

    }
}
