using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    //private CircleCollider2D circleCollider;
    private Unit unit;
    private Collider2D[] units;
    private LayerMask targetLayer;
    [SerializeField] GameObject rangedAttackProjectilePrefab;
    private float attackTimer = 0f;

    private void Start()
    {
        unit = GetComponent<Unit>();
        attackTimer = unit.GetAttackSpeed();
        string layerName = LayerMask.LayerToName(gameObject.layer);

        if (layerName == "Enemy")
        {
            targetLayer = LayerMask.GetMask("Ally");
        }
        else
        {
            targetLayer = LayerMask.GetMask("Enemy");
        }
        //circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        units = Physics2D.OverlapCircleAll(transform.position, unit.GetAttackRange(), targetLayer);

        if (units.Length > 0)
        {
            if (unit.GetTargetUnit())
            {
                foreach (Collider2D targetUnit in units)
                {
                    Unit target = targetUnit.GetComponent<Unit>();

                    if (target == unit.GetTargetUnit())
                    {
                        //Stop any movement to attack the target
                        unit.GetMovement().Stop();
                        if (attackTimer >= (1f / unit.GetAttackSpeed()) && unit.CanAttack())
                        {
                            if (!unit.IsStunned())
                            {
                                
                                //Wait for the attack to land to attack again
                                //unit.SetCanAttack(false);
                                //For ranged attack
                                if (!unit.IsMelee())
                                {
                                    GameObject projectile = Instantiate(rangedAttackProjectilePrefab, unit.transform.position, unit.transform.rotation);
                                    projectile.GetComponent<Projectile>().Initialize(target, unit, new Damage(Damage.Type.Physical, unit.GetDamage()), (Vector2.Distance(unit.transform.position, target.transform.position)) / (1f / unit.GetAttackSpeed()), new List<Ability>(unit.GetAttackModifiers()));
                                }
                                attackTimer = 0f;
                                unit.RemoveAttackModifiers();
                            } else
                            {
                                DisplayManager.errorMessage = "Stunned";
                            }
                        }
                    }
                }
            }
        }
    }
}
