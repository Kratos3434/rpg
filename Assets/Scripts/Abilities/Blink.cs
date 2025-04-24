using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : Ability
{
    private void Awake()
    {
        title = "Blink";
        description = "Allows the caster to blink to the target location";
        maxLevel = 4;
        affects.Add("-1");
        castRange = new List<float>(maxLevel) { 5, 6, 7, 8 };
    }

    public override void Activate()
    {
        OnTargetedAbility((target, targetPosition) =>
        {
            transform.position = targetPosition;
        });
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
