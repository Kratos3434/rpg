using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] Slider slider;


    public void SetMana(float mana)
    {
        slider.value = mana;
    }

    public void SetMaxMana(float maxMana)
    {
        slider.maxValue = maxMana;
        slider.value = maxMana;
    }
}
