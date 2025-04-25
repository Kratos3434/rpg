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

    public void SetValues(float maxDuration, float duration)
    {
        this.duration = duration;
        slider.maxValue = maxDuration;
        slider.value = duration;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate() {
        gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
}
