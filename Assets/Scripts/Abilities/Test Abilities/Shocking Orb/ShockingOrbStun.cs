using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockingOrbStun : Debuff
{
    private void Awake()
    {
        title = "Shocking Orb Stun";
        description = $"You are slowed stunned for {(int)duration}s";
    }

    public override void Activate()
    {
        targetUnit.Stun(duration);
        durationTimer = 0f;
        isActive = true;
    }

    public override void AddDebuff(Unit targetUnit)
    {
        throw new System.NotImplementedException();
    }

    public override void Dispel()
    {
        targetUnit.UnStun();
        Destroy(this);
    }
}
