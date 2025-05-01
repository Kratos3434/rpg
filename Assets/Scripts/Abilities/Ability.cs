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
    protected List<float> manaCost = new List<float>(1) { 0 };
    protected float cooldownTimer = 0f;
    protected Collider2D[] units;
    protected List<string> affects = new List<string>();
    protected List<float> healing;
    protected string affect;
    protected bool canCast = false;
    protected List<float> channelTime;
    protected float channelTimeTimer = 0f;
    protected float castTime = 0f;
    protected float castTimeTimer = 0f;
    LayerMask targetLayer;
    protected bool isChanneled = false;
    protected Vector3 targetPosition;

    private void Start()
    {
        try
        {
            int everything = int.Parse(affects[0]);
            targetLayer = everything;
            affect = "everything";
            Debug.Log("Target Layer set to everything");
        }
        catch
        {
            if (affects.Count > 0)
            {
                if (affects.Count > 1)
                {
                    targetLayer = LayerMask.GetMask(affects.ToArray());
                    affect = "Units";
                }
                else
                {

                    targetLayer = LayerMask.GetMask(affects[0]);
                }
            }
        }
        sourceUnit = GetComponent<Unit>();
    }

    private void Update()
    {
        if (castRange != null)
        {
            units = Physics2D.OverlapCircleAll(transform.position, castRange[0], targetLayer);
        }

        if (isActive)
        {
            if (!canCast && channelTimeTimer == 0f)
            {
                //Debug.Log("Casted");
                sourceUnit.GetMovement().Stop();
                castTimeTimer += Time.deltaTime;

                if (castTimeTimer >= castTime)
                {
                    sourceUnit.SetMana(sourceUnit.GetMana() - manaCost[currentLevel]);
                    canCast = true;
                    castTimeTimer = 0f;
                }
                if (sourceUnit.IsStunned())
                {
                    castTimeTimer = 0f;
                    isActive = false;
                    cooldownTimer = 0f;
                }
            }
            else
            {
                 if (channelTimeTimer > 0f)
                {
                    if (channelTimeTimer == channelTime[currentLevel])
                    {
                        sourceUnit.SetMana(sourceUnit.GetMana() - manaCost[currentLevel]);
                    }
                    channelTimeTimer -= Time.deltaTime;

                    if (channelTimeTimer <= 0f || sourceUnit.IsStunned())
                    {
                        Dispel();
                    }
                }
            }
        }

        if (!isActive && cooldownTimer > 0f)
        {

            if (cooldownTimer <= 0f)
            {
                cooldownTimer = 0f;
            }

            cooldownTimer -= Time.deltaTime;
        }
    }

    protected void OnTargetedAbility(Action<Unit, Vector3> action = null)
    {
        if (affect == "everything")
        {
            Vector2 direction = (MouseHover.GetTargetPosition() - transform.position).normalized;

            Vector2 offset = direction * castRange[0];

            Vector2 result = (Vector2)transform.position + offset;

            float distance = Vector2.Distance(transform.position, MouseHover.GetTargetPosition());

            sourceUnit.GetMovement().Stop();
            sourceUnit.SetCanAttack(false);

            if (distance > castRange[0])
            {
                //action(null, result);
                targetPosition = result;
            } else
            {
                //action(null, MouseHover.GetTargetPosition());
                targetPosition = MouseHover.GetTargetPosition();
            }
            //sourceUnit.SetMana(sourceUnit.GetMana() - manaCost[currentLevel]);
            isActive = true;
            cooldownTimer = cooldown[currentLevel];

        }   
        else if (MouseHover.GetHoveredUnit()) {
            try
            {
                //Debug.Log(affects[0]);
                if (LayerMask.LayerToName(gameObject.layer) != affects[0])
                {
                    if (MouseHover.GetHoveredUnit() == sourceUnit) throw new System.Exception("Ability cannot target self");
                    if (LayerMask.LayerToName(MouseHover.GetHoveredUnit().gameObject.layer) == LayerMask.LayerToName(gameObject.layer)) throw new Exception("Ability Cannot Target Allies");
                } else
                {
                    if (LayerMask.LayerToName(MouseHover.GetHoveredUnit().gameObject.layer) != LayerMask.LayerToName(gameObject.layer)) throw new Exception("Ability Cannot Target Enemies");
                }
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
                sourceUnit.SetCanAttack(false);
                //action(target, target.transform.position);
                //Debug.Log("Target set");
                targetUnit = target;
                isActive = true;

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
        canCast = false;
        durationTimer = 0f;
        cooldownTimer = cooldown[currentLevel];
        channelTimeTimer = 0f;
        sourceUnit.SetChanneledAbility(null);
    }

    protected void Cast(Action action)
    {
        if (canCast)
        {
            action();
            canCast = false;
            isActive = false;
        }
    }

    public float GetCooldownTimer() { return cooldownTimer; }

    public float GetDuration()
    {
        return duration[currentLevel];
    }

    public float GetDurationTimer() { return durationTimer; }

    public float GetCastTime()
    {
        return castTime;
    }

    protected string InverseLayer()
    {
        string layerName = LayerMask.LayerToName(gameObject.layer);

        if (layerName == "Enemy")
        {
            return "Ally";
        }
        else
        {
            return "Enemy";
        }
    }
    public float GetCastTimeTimer()
    {
        return castTimeTimer;
    }

    public float GetManaCost()
    {
        return manaCost[currentLevel];
    }

    public float GetChannelTimeTimer()
    {
        return channelTimeTimer;
    }

    public float GetChannelTime()
    {
        return channelTime[currentLevel];
    }

    public void ReSetLayer()
    {
        try
        {
            int everything = int.Parse(affects[0]);
            targetLayer = everything;
            affect = "everything";
            Debug.Log("Target Layer set to everything");
        }
        catch
        {
            if (affects.Count > 0)
            {
                if (affects.Count > 1)
                {
                    targetLayer = LayerMask.GetMask(affects.ToArray());
                    affect = "Units";
                }
                else
                {
                    affects[0] = InverseLayer();
                    targetLayer = LayerMask.GetMask(affects[0]);
                    //Debug.Log("Target layer changed");
                }
            }
        }
    }
}
