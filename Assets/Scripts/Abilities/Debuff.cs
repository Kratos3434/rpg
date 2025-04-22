using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a type of spell that adds negative effectg to the target. Must be inherited by debuff type spells
/// </summary>
public abstract class Debuff : Spell
{
    protected float duration;
    protected float amount;

    public void Initialize(Unit targetUnit, float duration, float amount)
    {
        this.targetUnit = targetUnit;
        this.duration = duration;
        this.amount = amount;
    }
}
