using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empower : Ability
{

    private void Awake()
    {
        icon = "Icons/Abilities/Empower";
        title = "Empower";
        description = "Your next attack will deal bonus damage";
        currentLevel = 0;
        maxLevel = 4;
        damage = new List<float>(maxLevel)
        {
            10,
            20,
            30,
            40
        };

        duration = new List<float>(maxLevel) { 10, 10, 10, 10 };

        cooldown = new List<float>(maxLevel) { 10, 9, 8, 7 };

        manaCost = new List<float>(maxLevel) { 100, 110, 120, 140 };

        damageType = Damage.Type.Physical;
    }

    private void LateUpdate()
    {
        if (isActive)
        {
            durationTimer += Time.deltaTime;

            if (durationTimer >= duration[currentLevel])
            {
                Dispel();
                sourceUnit.RemoveAttackModifier(this);
            }
        }
    }

    public override void Activate()
    {
        if (!isActive)
        {
            sourceUnit.SetBonusDamage(sourceUnit.GetBonusDamage() + damage[currentLevel]);
            sourceUnit.AddAttackModifier(this);
            //cooldownTimer = cooldown[currentLevel];
            isActive = true;
        }
    }

    public override void Dispel()
    {
        OnDispel(() =>
        {
            sourceUnit.SetBonusDamage(sourceUnit.GetBonusDamage() - damage[currentLevel]);
        });
    }

    public override void AddDebuff(Unit targetUnit)
    {
        Slow slow = targetUnit.GetComponent<Slow>();

        if (slow)
        {
            slow.ResetDuration();
        } else
        {
            Slow s = targetUnit.gameObject.AddComponent<Slow>();
            targetUnit.AddDebuff(s);
            s.Initialize(targetUnit, 3f, .80f);
            s.Activate();
        }

    }
}
