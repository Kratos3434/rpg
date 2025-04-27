using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : Ability
{
    private int cloneNumber = 0;

    private void Awake()
    {
        title = "Clone";
        description = "Create a clone of yourself that can use abilities that you can control";
        maxLevel = 4;
        cooldown = new List<float>(maxLevel) { 5, 5, 5, 5 };
        ///castTime = .1f;
    }
    public override void Activate()
    {
        isActive = true;
        cloneNumber++;
        sourceUnit.CreateClone();
        isActive = false;
        cooldownTimer = cooldown[currentLevel];
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
