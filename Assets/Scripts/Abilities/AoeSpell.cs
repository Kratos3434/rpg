using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeSpell : Ability
{
    private void Awake()
    {
        title = "Aoe Spell";
        description = "Damages all enemies around you";
        affects.Add("Enemy");

        damage = new List<float>(maxLevel) { 500, 550, 600, 650 };

        damageType = Damage.Type.Magical;

        castRange = new List<float>(maxLevel) { 5, 5, 5, 5 };
    }

    public override void Activate()
    {
        sourceUnit.GetMovement().Stop();
        foreach (Collider2D unit in units)
        {
            Unit u = unit.GetComponent<Unit>();

            if (u)
            {
                u.TakeDamage(new Damage(damageType, damage[currentLevel]));
            }
        }
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
