using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanneledAbility : Ability
{
    private void Awake()
    {
        title = "Channeled Ability";
        description = "CHANNELED - Heal over the duration";
        maxLevel = 4;
        currentLevel = 0;
        manaCost = new List<float>(maxLevel) { 50, 100, 150, 200 };
        cooldown = new List<float>(maxLevel) { 10, 9, 8, 7 };
        icon = "Icons/Abilities/Heal";
        channelTime = new List<float>(maxLevel) { 10, 10, 10, 10 };
        healing = new List<float>(maxLevel) { 50, 60, 70, 80 };
    }

    private void LateUpdate()
    {
        if (isActive)
        {
            if (sourceUnit.GetMovement().IsMoving())
            {
                Dispel();
            }
        }
    }

    public override void Activate()
    {
        if (!isActive)
        {
            isActive = true;
            channelTimeTimer = channelTime[currentLevel];
            sourceUnit.SetBonusHealthRegen(sourceUnit.GetBonusHealthRegen() + healing[currentLevel]);
            sourceUnit.SetChanneledAbility(this);
            sourceUnit.GetMovement().Stop();
        }
    }

    public override void AddDebuff(Unit targetUnit)
    {
        throw new System.NotImplementedException();
    }

    public override void Dispel()
    {
        OnDispel(() =>
        {
            sourceUnit.SetBonusHealthRegen(sourceUnit.GetBonusHealthRegen() - healing[currentLevel]);
        });
    }
}
