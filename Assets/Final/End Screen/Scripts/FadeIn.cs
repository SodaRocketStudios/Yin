using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private bool fade = false;

    private Image image;

    [SerializeField]
    private float fadeTime = 1;
    private float fadeAmount;

    private void Awake()
    {
        image = GetComponent<Image>();

        fadeAmount = 1/fadeTime;
    }

    private void OnEnable()
    {
        fade = true;
        Color color = image.color;
        color.a = 0;
        image.color = color;
    }

    private void Update() {
        if(fade == true)
        {
            float fadeStep = fadeAmount*Time.deltaTime;
            image.color = image.color + new Color(0, 0, 0, fadeStep);
        }
    }
}
