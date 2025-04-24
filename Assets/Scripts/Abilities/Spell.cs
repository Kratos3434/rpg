using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class of all spell types: Ability, Debuff, Buff
/// </summary>
public abstract class Spell : MonoBehaviour
{
    protected string title;
    protected string description;
    protected bool dispellable = true;
    protected Damage.Type damageType;
    protected Unit sourceUnit;
    protected Unit targetUnit;
    protected bool isActive = false;
    protected float durationTimer = 0f;
    protected string icon = "";

    /// <summary>
    /// This contains the logic of the spell
    /// </summary>
    public abstract void Activate();

    /// <summary>
    /// This allows the spell to be dispelled removing any effects it has
    /// </summary>
    public abstract void Dispel();

    public abstract void AddDebuff(Unit targetUnit);

    public void SetSourceUnit(Unit sourceUnit) {  this.sourceUnit = sourceUnit; }

    public void SetTargetUnit(Unit targetUnit) {  this.targetUnit = targetUnit; }

    public void ResetDuration()
    {
        durationTimer = 0f;
    }

    public string GetIcon() { return icon; }
}
