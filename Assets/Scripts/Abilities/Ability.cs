using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class of all spells/abilities used by every unit that has an ability
/// </summary>
public abstract class Ability : Spell
{
    protected int currentLevel = 0;
    protected int maxLevel = 4;
    protected List<float> damage;
    protected List<float> duration;
    protected List<float> castRange;
    protected List<float> cooldown;
    protected float cooldownTimer = 0f;
    protected Collider2D[] units;
    protected List<string> affects = new List<string>();
    protected string affect;
    LayerMask targetLayer;

    private void Start()
    {
        //Logic to set which unit this ability will affect
        try
        {
            int everything = int.Parse(affects[0]);
            targetLayer = everything;
            affect = "everything";
            Debug.Log("Target Layer set to everything");
        } catch
        {
            if (affects.Count > 1)
            {
                targetLayer = LayerMask.GetMask(affects.ToArray());
                affect = "Units";
            } else
            {
                //Inverse the LayerMask
                string layerName = LayerMask.LayerToName(gameObject.layer);

                if (layerName == "Enemy")
                {
                    targetLayer = LayerMask.GetMask("Ally");
                } else {
                    targetLayer = LayerMask.GetMask("Enemy");
                }
            }
        }
        sourceUnit = GetComponent<Unit>();
    }

    private void Update()
    {
        units = Physics2D.OverlapCircleAll(transform.position, castRange[0], targetLayer);
    }

    private void LateUpdate()
    {
        if (!isActive && cooldownTimer > 0f)
        {

            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
            }

            cooldownTimer -= Time.deltaTime;
        }
    }

    protected void OnTargetedAbility(Action<Unit, Vector3> action)
    {
        if (affect == "everything")
        {
            Vector2 direction = (MouseHover.GetTargetPosition() - transform.position).normalized;

            Vector2 offset = direction * castRange[0];

            Vector2 result = (Vector2)transform.position + offset;

            float distance = Vector2.Distance(transform.position, MouseHover.GetTargetPosition());

            sourceUnit.GetMovement().Stop();

            if (distance > castRange[0])
            {
                action(null, result);
            } else
            {
                action(null, MouseHover.GetTargetPosition());
            }

            cooldownTimer = cooldown[currentLevel];

        }   
        else if (MouseHover.GetHoveredUnit()) {
            try
            {
                if (MouseHover.GetHoveredUnit() == sourceUnit) throw new System.Exception("Ability cannot target self");
                if (LayerMask.LayerToName(MouseHover.GetHoveredUnit().gameObject.layer) == LayerMask.LayerToName(gameObject.layer)) throw new Exception("Ability Cannot Target Allies");
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

                sourceUnit.GetMovement().Stop();

                action(target, target.transform.position);

                cooldownTimer = cooldown[currentLevel];
            }
            catch (System.Exception e)
            {
                DisplayManager.errorMessage = e.Message;
            }
        }   
        else
        {
            DisplayManager.errorMessage = "No Target";
        }
    }

    protected void OnDispel(Action action)
    {
        action();
        isActive = false;
        durationTimer = 0f;
        cooldownTimer = cooldown[currentLevel];
    }

    public float GetCooldownTimer() { return cooldownTimer; }
}
