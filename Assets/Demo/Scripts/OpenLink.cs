using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OpenLink : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private string url;

    [SerializeField]
    private OnClickEvent onClick;

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick.Invoke(url);
    }
}

[System.Serializable]
public class OnClickEvent : UnityEvent<string> {}
