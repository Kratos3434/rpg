using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonicConversion : Ability
{
    private void Awake()
    {
        affects.Add(InverseLayer());
        title = "Demonic Conversion";
        description = "Convert an enemy and turn them into an ally";
        maxLevel = 4;
        cooldown = new List<float>(maxLevel) { 5, 5, 5, 5 };
        manaCost = new List<float>(maxLevel) { 100, 150, 200, 250 };
        castRange = new List<float>(1) { 5f };
    }

    private void LateUpdate()
    {
        Cast(() =>
        {
            targetUnit.gameObject.layer = gameObject.layer;
            foreach (Ability ability in targetUnit.GetAbilities())
            {
                ability.ReSetLayer();
            }
        });
    }

    public override void Activate()
    {
        OnTargetedAbility();
    }

    public override void AddDebuff(Unit targetUnit)
    {
        throw new System.NotImplementedException();
    }

    public override void Dispel()
    {
        throw new System.NotImplementedException();
    }
}
