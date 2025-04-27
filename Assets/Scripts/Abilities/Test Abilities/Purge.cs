using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purge : Ability
{
    private void Awake()
    {
        affects.Add(LayerMask.LayerToName(gameObject.layer));
        title = "Purge";
        description = "Dispel the target ally or yourself";
        maxLevel = 4;
        cooldown = new List<float>(maxLevel) { 5, 4, 3, 2, 1 };
        castRange = new List<float>(1) { 5f };
        castTime = .1f;
    }

    private void LateUpdate()
    {
        Cast(() =>
        {
            targetUnit.RemoveAllDebuff();
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
