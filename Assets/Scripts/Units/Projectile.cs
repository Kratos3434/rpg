using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Damage damage;
    private List<Ability> modifiers = null;
    private Unit sourceUnit;
    private Unit targetUnit;
    private float speed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, speed * Time.deltaTime);
        //So that the projectile always faces the targets angle
        Vector2 direction = targetUnit.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            //Debug.Log("Collision detected");
            Unit unit = collision.GetComponent<Unit>();
            //Debug.Log(unit.name);
            if (unit)
            {
                if (unit == targetUnit)
                {
                    targetUnit.TakeDamage(damage);
                    if (modifiers != null)
                    {
                        foreach (Ability ability in modifiers)
                        {
                            ability.AddDebuff(targetUnit);
                        }
                    }
                    sourceUnit.SetCanAttack(true);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Initialize(Unit targetUnit, Unit sourceUnit, Damage damage, float speed, List<Ability> modifiers)
    {
        this.targetUnit = targetUnit;
        this.damage = damage;
        this.sourceUnit = sourceUnit;
        this.modifiers = modifiers;
        this.speed = speed;
    }
}
