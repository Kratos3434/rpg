using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockingOrb : Ability
{
    private GameObject projectilePrefab;

    private void Awake()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Abilities/Shocking Orb/Shocking Orb Projectile");
        affects.Add("Enemy");
        title = "Shocking Orb";
        description = "Fires an orb to the target enemy dealing damage";
        maxLevel = 4;
        damage = new List<float>(maxLevel) { 50, 100, 150, 200 };
        damageType = Damage.Type.Magical;
        castRange = new List<float>(1) { 5f };
    }

    public override void Activate()
    {
        OnTargetedAbility((target, targetPosition) =>
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().Initialize(target, sourceUnit, new Damage(damageType, damage[currentLevel]), 4f, null);
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
