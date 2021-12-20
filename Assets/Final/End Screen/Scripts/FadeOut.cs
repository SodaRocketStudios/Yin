using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeOut : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private float fadeTime = 1;
    private float fadeAmount;

    public UnityEvent OnComplete;
    private bool complete = true;

    private void Awake()
    {
        image = GetComponent<Image>();

        fadeAmount = 1/fadeTime;
    }

    public void Play(float delay)
    {
        StartCoroutine(PlayAfterSeconds(delay));
    }

    private IEnumerator PlayAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        complete = false;
    }

    private void Update() {
        if(complete == false)
        {
            float fadeStep = fadeAmount*Time.deltaTime;
            image.color = image.color - new Color(0, 0, 0, fadeStep);

            if(image.color.a <= 0)
            {
                OnComplete.Invoke();
                complete = true;
            }
        }
    }
}