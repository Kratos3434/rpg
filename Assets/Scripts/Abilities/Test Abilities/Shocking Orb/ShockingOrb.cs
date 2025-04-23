using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockingOrb : Ability
{
    private Collider2D[] units;
    LayerMask targetLayer;
    private GameObject projectilePrefab;

    private void Awake()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefabs/Abilities/Shocking Orb/Shocking Orb Projectile");
        targetLayer = LayerMask.GetMask("Enemy");
        title = "Shocking Orb";
        description = "Fires an orb to the target enemy dealing damage";
        maxLevel = 4;
        damage = new List<float>(maxLevel) { 50, 100, 150, 200 };
        damageType = Damage.Type.Magical;
        castRange = new List<float>(1) { 5f };
    }

    private void Update()
    {
        units = Physics2D.OverlapCircleAll(transform.position, castRange[0], targetLayer);

    }

    public override void Activate()
    {
        if (MouseHover.GetHoveredUnit()) {
            try
            {
                if (MouseHover.GetHoveredUnit() == sourceUnit) throw new System.Exception("Ability cannot target self");
                if (units.Length == 0) throw new System.Exception("Target out of range");

                Unit target = null;

                foreach (Collider2D unitCollider in units)
                {
                    Unit u = unitCollider.GetComponent<Unit>();

                    if (u == MouseHover.GetHoveredUnit())
                    {
                        target = u;
                        break;
                    }
                }

                if (!target) throw new System.Exception("Target out of range");

                GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                projectile.GetComponent<Projectile>().Initialize(target, sourceUnit, new Damage(damageType, damage[currentLevel]), 4f, null);
            } catch (System.Exception e)
            {
                DisplayManager.errorMessage = e.Message;
            }
        } else
        {
            DisplayManager.errorMessage = "No Target";
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
