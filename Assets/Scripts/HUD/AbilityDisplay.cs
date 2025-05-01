using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityDisplay : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] 
    private string placeholderPath = "Icons/Abilities/Empty";
    private Ability ability;
    [SerializeField] TextMeshProUGUI cooldownTimer;
    [SerializeField] GameObject cover;
    [SerializeField] StatusBar statusBar;
    [SerializeField] GameObject stunIcon;
    [SerializeField] GameObject notEnoughManaIcon;
    [SerializeField] GameObject channelingStatus;
    [SerializeField] StatusBar channelingStatusBar;
    private Unit unit;

    private void Update()
    {

        if (unit.IsStunned())
        {
            if (!stunIcon.activeSelf)
            {
                stunIcon.SetActive(true);
            }
        }
        else
        {
            if (stunIcon.activeSelf)
            {
                stunIcon.SetActive(false);
            }
        }

        if (ability)
        {
            if (unit.GetMana() < ability.GetManaCost())
            {
                notEnoughManaIcon.SetActive(true);
            }
            else
            {
                notEnoughManaIcon.SetActive(false);
            }

            if (ability.GetDurationTimer() > 0f)
            {
                if (!statusBar.IsActive())
                {
                    statusBar.Activate();
                    cover.SetActive(true);
                }
                statusBar.SetValues(ability.GetDuration(), ability.GetDuration() - ability.GetDurationTimer());
            } else
            {
                statusBar.Deactivate();
            }

            if (ability.GetCastTimeTimer() > 0f)
            {
                if (!statusBar.IsActive())
                {
                    statusBar.Activate();
                    cover.SetActive(true);
                    //cooldownTimer.gameObject.SetActive(false);
                }
                statusBar.SetValues(ability.GetCastTime(), ability.GetCastTimeTimer());
            }

            if (ability.GetCooldownTimer() > 0f && ability.GetCastTimeTimer() == 0f)
            {
                if (!cooldownTimer.gameObject.activeSelf)
                {
                    cover.SetActive(true);
                    cooldownTimer.gameObject.SetActive(true);
                }

                cooldownTimer.text = $"{(ability.GetCooldownTimer() < 1 ? ability.GetCooldownTimer().ToString("F1") : (int)ability.GetCooldownTimer())}";
            }
            else
            {
                if (cooldownTimer.gameObject.activeSelf)
                {
                    cover.SetActive(false);
                    cooldownTimer.gameObject.SetActive(false);
                }
            }

            if (ability.GetChannelTimeTimer() > 0f)
            {
                cover.SetActive(true);
                channelingStatus.SetActive(true);
                channelingStatusBar.SetValues(ability.GetChannelTime(), ability.GetChannelTimeTimer());
            } else
            {
                channelingStatus.SetActive(false);
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

    public void Set(Ability ability, Unit unit)
    {
        this.ability = ability;
        this.unit = unit;

        if (ability)
        {
            cover.SetActive(false);
            SetIcon(ability.GetIcon());
        } else
        {
            cooldownTimer.gameObject.SetActive(false);
            cover.SetActive(false);
            stunIcon.SetActive(false);
            statusBar.Deactivate();
            SetIcon("Icons/Abilities/Empty");
        }
    }
}
