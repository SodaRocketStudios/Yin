using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DynamicCollider : MonoBehaviour
{
    private bool isChaosElement = false;
    private bool isBetweenWorlds = false;

    private Vector2[] defaultPoints;
    private Vector2[] worldPoints;

    private PolygonCollider2D polyCollider;

    Vector2 topRight;
    Vector2 bottomLeft;

    private int noCollisionLayer;
    private int defaultLayer;

    private Collider2D maskCollider;

    private void Awake()
    {
        polyCollider = GetComponent<PolygonCollider2D>();

        defaultPoints = new Vector2[polyCollider.GetTotalPointCount()];
        polyCollider.points.CopyTo(defaultPoints, 0);

        worldPoints = new Vector2[defaultPoints.Length];
        for(int i = 0; i < defaultPoints.Length; i++)
        {
            worldPoints[i] = transform.TransformPoint(defaultPoints[i]);
        }

        noCollisionLayer = LayerMask.NameToLayer("No Collision");
        defaultLayer = gameObject.layer;

        topRight = new Vector2((polyCollider.bounds.center + polyCollider.bounds.extents).x, (polyCollider.bounds.center + polyCollider.bounds.extents).y);
        bottomLeft = new Vector2((polyCollider.bounds.center - polyCollider.bounds.extents).x, (polyCollider.bounds.center - polyCollider.bounds.extents).y);

        isChaosElement = gameObject.layer == LayerMask.NameToLayer("Chaos Collision");

        if(isChaosElement)
        {
            gameObject.layer = noCollisionLayer;
        }

        maskCollider = GameObject.FindGameObjectWithTag("Mask").GetComponent<Collider2D>();
    }

    private void Update()
    {
        CheckBounds(maskCollider);

        if(isBetweenWorlds == true)
        {
            MovePoints(maskCollider);

            gameObject.layer = defaultLayer;
            return;
        }

        if(isChaosElement == true)
        {
            gameObject.layer = defaultLayer;
        }
        else
        {
            gameObject.layer = noCollisionLayer;
        }

        polyCollider.points = defaultPoints;
    }

    private void CheckBounds(Collider2D other)
    {
        bool containsTopRight = other.bounds.Contains(topRight);
        bool containsBottomLeft = other.bounds.Contains(bottomLeft);

        isBetweenWorlds = containsBottomLeft ^ containsTopRight;
    }

    private void MovePoints(Collider2D other)
    {
        Vector2[] points = new Vector2[defaultPoints.Length];

        polyCollider.points.CopyTo(points, 0);

        for(int i = 0; i < points.Length; i++)
        {
            bool containedByMask = other.bounds.Contains(worldPoints[i]);

            bool movePoint = containedByMask ^ isChaosElement;

            Vector2 neighbor = GetNeighbor(points, i);

            if(movePoint == true)
            {
                // Get the right edge of the mask
                float maskEdgePosition = other.bounds.center.x + other.bounds.extents.x;

                Vector2 neighborWorldPosition = transform.TransformPoint(neighbor);

                bool matchNeighbor = other.bounds.Contains(neighborWorldPosition) ^ isChaosElement;

                if(matchNeighbor)
                {
                    points[i] = neighbor;
                    continue;
                }

                Vector2 moveDirection = (neighborWorldPosition - worldPoints[i]).normalized;
                float deltaX = maskEdgePosition - worldPoints[i].x;
                float angle = Vector2.Angle(Vector2.right, moveDirection);
                float moveDistance = deltaX/(Mathf.Cos(angle*Mathf.Deg2Rad));

                points[i] = transform.InverseTransformPoint(worldPoints[i] + moveDirection*moveDistance);

                Debug.DrawLine(worldPoints[i], worldPoints[i] + moveDirection*moveDistance, Color.red);
            }
            else
            {
                points[i] = defaultPoints[i];
            }
        }

        polyCollider.points = points;
    }

    private Vector2 GetNeighbor(Vector2[] points, int index)
    {
        Vector2 neighbor;

        int[] neighborPoints = new int[2];
        neighborPoints[0] = (index+1)%points.Length;
        neighborPoints[1] = (index-1)%points.Length;

        if(neighborPoints[1] < 0)
        {
            neighborPoints[1] = points.Length + neighborPoints[1];
        }

        // Get the rightmost neighbor for order elements and leftmost for chaos elements
        if(isChaosElement == false)
        {
            neighbor = (worldPoints[neighborPoints[0]].x >= worldPoints[neighborPoints[1]].x)?points[neighborPoints[0]]:points[neighborPoints[1]];
        }
        else
        {
            neighbor = (worldPoints[neighborPoints[0]].x <= worldPoints[neighborPoints[1]].x)?points[neighborPoints[0]]:points[neighborPoints[1]];
        }

        return neighbor;
    }
}