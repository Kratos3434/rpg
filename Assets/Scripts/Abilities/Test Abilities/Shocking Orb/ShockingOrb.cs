using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockingOrb : Ability
{
    private GameObject projectilePrefab;

    private void Awake()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Abilities/Shocking Orb/Shocking Orb Projectile");
        icon = "Icons/Abilities/Shocking_Orb_Icon";
        affects.Add(InverseLayer());
        title = "Shocking Orb";
        description = "Fires an orb to the target enemy dealing damage and stunning them";
        maxLevel = 4;
        damage = new List<float>(maxLevel) { 50, 100, 150, 200 };
        manaCost = new List<float>(maxLevel) { 50, 100, 150, 200 };
        damageType = Damage.Type.Magical;
        castRange = new List<float>(1) { 5f };
        cooldown = new List<float>(maxLevel) { 10, 9, 8, 7 };
        castTime = .1f;
    }

    private void LateUpdate()
    {
        Cast(() =>
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.GetComponent<Projectile>().Initialize(targetUnit, sourceUnit, new Damage(damageType, damage[currentLevel]), 4f, new List<Ability>(1) { this });
        });
    }

    public override void Activate()
    {
        OnTargetedAbility();
    }

    public override void AddDebuff(Unit targetUnit)
    {
        ShockingOrbStun shockingOrbStun = targetUnit.gameObject.GetComponent<ShockingOrbStun>();

        if (shockingOrbStun)
        {
            shockingOrbStun.Activate();
        } else
        {
            ShockingOrbStun s = targetUnit.gameObject.AddComponent<ShockingOrbStun>();
            targetUnit.AddDebuff(s);
            s.Initialize(targetUnit, 10f);
            s.Activate();
        }
    }

    public override void Dispel()
    {
        throw new System.NotImplementedException();
    }
}
