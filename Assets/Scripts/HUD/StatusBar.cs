using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    private Slider slider;
    private float duration = 0f;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        if (duration > 0f)
        {
            slider.value = duration;

            duration -= Time.deltaTime;
        } else
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void Activate(float duration)
    {
        this.duration = duration;
        slider.maxValue = duration;
        slider.value = duration;
        gameObject.SetActive(true);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
