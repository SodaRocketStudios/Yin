using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TransitionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Color defaultColor;

    [SerializeField]
    private Color transitionColor;

    [SerializeField]
    private Image linkedButton;

    public void OnPointerEnter(PointerEventData eventData)
    {
        linkedButton.color = transitionColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        linkedButton.color = defaultColor;
    }

    private void Update()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}