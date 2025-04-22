using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empower : Ability
{

    private void Awake()
    {
        sourceUnit = GetComponent<Unit>();
        title = "Empower";
        description = "Your next attack will deal bonus damage";
        currentLevel = 3;
        maxLevel = 4;
        damage = new List<float>(maxLevel)
        {
            10,
            20,
            30,
            40
        };

        duration = new List<float>(maxLevel) { 10, 10, 10, 10 };

        damageType = Damage.Type.Physical;
    }

    private void Update()
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
            isActive = true;
        } else
        {
            durationTimer = 0f;
        }
    }

    public override void Dispel()
    {
        sourceUnit.SetBonusDamage(sourceUnit.GetBonusDamage() - damage[currentLevel]);
        isActive = false;
        durationTimer = 0f;
        //sourceUnit.RemoveAttackModifier(this);
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
