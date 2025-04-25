using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : Debuff
{
    private void Awake()
    {
        title = "Empower Slow";
        description = $"You are slowed by {amount * 100}% for {duration} seconds";
    }

    //private void Update()
    //{
    //    if (isActive)
    //    {
    //        durationTimer += Time.deltaTime;

    //        if (durationTimer >= duration)
    //        {
    //            Dispel();
    //        }
    //    }
    //}

    public override void Activate()
    {
        targetUnit.SetBonusMovementSpeed(targetUnit.GetBonusMovementSpeed() - (targetUnit.GetBonusMovementSpeed() * amount));
        isActive = true;
    }

    public override void Dispel()
    {
        targetUnit.SetBonusMovementSpeed(targetUnit.GetBonusMovementSpeed() / (1 - amount));
        targetUnit.RemoveDebuff(this);
        Destroy(this);
    }

    public override void AddDebuff(Unit targetUnit)
    {
        throw new System.NotImplementedException();
    }
}
