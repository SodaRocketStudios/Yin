using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEditor : MonoBehaviour
{
    private List<Obstacle> obstacles;

    private BoxCollider2D maskCollider;

    private void Awake()
    {
        obstacles = new List<Obstacle>();
        maskCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PolygonCollider2D polygonCollider;
        if(other.TryGetComponent<PolygonCollider2D>(out polygonCollider) == false)
        {
            return;
        }

        // Create a new collider
        Obstacle newObstacle = new Obstacle(other.GetComponent<PolygonCollider2D>());
        newObstacle.index = obstacles.Count;
        obstacles.Add(newObstacle);

        // If it is a chaos object
        if(other.gameObject.layer == LayerMask.NameToLayer("Chaos Collision"))
        {
            newObstacle.isChaosElement = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
    }

    private class Obstacle
    {
        public int index;
        public Vector2[] points;
        public Vector2[] defaultPoints;
        public bool isChaosElement = false;
        public bool betweenWorlds;

        public Obstacle(PolygonCollider2D collider)
        {
            points = new Vector2[collider.GetTotalPointCount()];
            defaultPoints = new Vector2[collider.GetTotalPointCount()];

            collider.points.CopyTo(defaultPoints, 0);
        }
    }
}
