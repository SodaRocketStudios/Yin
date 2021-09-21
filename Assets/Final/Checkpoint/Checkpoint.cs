using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    private Point _point;
    public Point point{ get{return _point;}}


    private void Awake()
    {
        _point.hasBeenActivated = false;
        _point.position = transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") == true)
        {
            if(_point.hasBeenActivated == false)
            {
                // Set this checkpoint as the current checkpoint
                CheckpointManager.Instance.CurrentCheckpoint = this.point;
                
                _point.hasBeenActivated = true;
            }
        }
    }

    [System.Serializable]
    public struct Point
    {
        public float position;

        public bool hasBeenActivated;
    }
}
