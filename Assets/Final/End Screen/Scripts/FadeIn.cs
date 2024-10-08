using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeIn : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private float fadeTime = 1;
    private float fadeAmount;

    public UnityEvent OnComplete;
    private bool complete;

    private void Awake()
    {
        image = GetComponent<Image>();

        fadeAmount = 1/fadeTime;
    }

    private void OnEnable()
    {
        complete = false;
        Color color = image.color;
        color.a = 0;
        image.color = color;
    }

    private void Update() {
        if(complete == false)
        {
            float fadeStep = fadeAmount*Time.deltaTime;
            image.color = image.color + new Color(0, 0, 0, fadeStep);

            if(image.color.a >= 1)
            {
                OnComplete.Invoke();
                complete = true;
            }
        }
    }
}