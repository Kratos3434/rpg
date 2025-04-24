using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityDisplay : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] 
    private string placeholderPath = "Icons/Abilities/CycHzgAWEAAZErb";
    private Ability ability;
    [SerializeField] TextMeshProUGUI cooldownTimer;
    [SerializeField] GameObject cover;

    private void Update()
    {
        if (ability.GetCooldownTimer() > 0f)
        {
            if (!cooldownTimer.gameObject.activeSelf)
            {
                cover.SetActive(true);
                cooldownTimer.gameObject.SetActive(true);
            }

            cooldownTimer.text = $"{(int)ability.GetCooldownTimer()}";
        } else
        {
            if (cooldownTimer.gameObject.activeSelf)
            {
                cover.SetActive(false);
                cooldownTimer.gameObject.SetActive(false);
            }
        }
    }

    public void SetIcon(string path)
    {
        string iconPath;
        if (path == "")
        {
            iconPath = placeholderPath;
        }
        else
        {
            iconPath = path;
        }

        Sprite loadedSprite = Resources.Load<Sprite>(iconPath);

        if (loadedSprite)
        {
            icon.sprite = loadedSprite;
        }
        else
        {
            Debug.Log("Icon path is invalid");
        }
    }

    public void Set(Ability ability)
    {
        if (ability)
        {
            this.ability = ability;
            SetIcon(ability.GetIcon());
        } else
        {
            SetIcon("");
        }
    }
}
