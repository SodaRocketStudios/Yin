using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnTriggerEvent;

    [SerializeField, Tooltip("Check this if you only want the trigger to only activate the first time.")]
    private bool triggerOnce = false;

    private bool hasBeenTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(triggerOnce == false || hasBeenTriggered == false)
        {
            OnTriggerEvent.Invoke();
        }
        
    }
}
