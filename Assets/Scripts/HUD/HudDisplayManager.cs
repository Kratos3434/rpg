using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudDisplayManager : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] HealthBar healthBar;
    [SerializeField] ManaBar manaBar;
    [SerializeField] TextMeshProUGUI health;
    [SerializeField] TextMeshProUGUI healthRegen;
    [SerializeField] TextMeshProUGUI mana;
    [SerializeField] TextMeshProUGUI maxMana;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] GameObject errorMessage;
    [SerializeField] TextMeshProUGUI errorMessageText;
    [SerializeField] AbilityDisplay ability1;
    [SerializeField] AbilityDisplay ability2;
    [SerializeField] AbilityDisplay ability3;


    private float errorMessageTimer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        errorMessage.SetActive(false);
        manaBar.SetMaxMana(unit.GetMaxMana());
        healthBar.SetMaxHealth(unit.GetMaxHealth());
        SetValues();
        ability1.Set(unit.GetAbility(0));
        ability2.Set(unit.GetAbility(1));
        ability3.Set(unit.GetAbility(2));
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.SetMana(unit.GetMana());
        healthBar.SetHealth(unit.GetHealth());
        SetValues();

        if (DisplayManager.errorMessage != null)
        {
            errorMessageTimer += Time.deltaTime;

            errorMessage.SetActive(true);
            errorMessageText.text = DisplayManager.errorMessage;

            if (errorMessageTimer >= 1f) {
                DisplayManager.errorMessage = null;
                errorMessage.SetActive(false);
                errorMessageTimer = 0f;
            }
        }
    }

    private void SetValues()
    {
        health.text = $"{(int)unit.GetHealth()}/{(int)unit.GetMaxHealth()}";
        healthRegen.text = $"+{unit.GetHealthRegen():F1}";
        mana.text = $"{(int)unit.GetMana()}/{(int)unit.GetMaxMana()}";
        maxMana.text = $"+{unit.GetManaRegen():F1}";
        damage.text = $"{(int)unit.GetBaseDamage()} + {(int)unit.GetBonusDamage()} Damage";
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
        manaBar.SetMaxMana(unit.GetMaxMana());
        healthBar.SetMaxHealth(unit.GetMaxHealth());
        ability1.Set(unit.GetAbility(0));
        ability2.Set(unit.GetAbility(1));
        ability3.Set(unit.GetAbility(2));
        //SetValues();
    }

    public Unit GetUnit()
    {
        return unit;
    }
}
