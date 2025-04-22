using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public enum Type
    {
        Physical,
        Magical,
        Pure
    }

    private Type type;
    private float damage;

    public Damage(Type type, float damage)
    {
        this.type = type;
        this.damage = damage;
    }

    public Type getType()
    {
        return type;
    } 

    public float getDamage() { return damage; }
}
