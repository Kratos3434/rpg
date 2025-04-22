using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    private float damage;
    private Unit sourceUnit;
    private Unit targetUnit;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetUnit.transform.position, sourceUnit.GetAttackSpeed() * Time.deltaTime);
        //So that the projectile always faces the targets angle
        Vector2 direction = targetUnit.transform.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision)
        {
            Debug.Log("Collision detected");
            Unit unit = collision.GetComponent<Unit>();
            Debug.Log(unit.name);
            if (unit)
            {
                if (unit == targetUnit)
                {
                    sourceUnit.SetCanAttack(true);
                    Destroy(gameObject);
                }
            }
        }
    }

    public void Initialize(Unit targetUnit, Unit sourceUnit, float damage)
    {
        this.targetUnit = targetUnit;
        this.damage = damage;
        this.sourceUnit = sourceUnit;
    }
}
