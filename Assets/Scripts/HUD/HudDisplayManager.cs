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


    // Start is called before the first frame update
    void Start()
    {
        manaBar.SetMaxMana(unit.GetMaxMana());
        healthBar.SetMaxHealth(unit.GetMaxHealth());
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        manaBar.SetMana(unit.GetMana());
        healthBar.SetHealth(unit.GetHealth());
        SetValues();
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
    }

    public Unit GetUnit()
    {
        return unit;
    }
}
