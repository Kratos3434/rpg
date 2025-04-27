using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Unit
{
    private void Awake()
    {
        Initialize(500, 500, 50);
        baseAttackSpeed = 3f;
        AddAbility<ShockingOrb>();
        AddAbility<Empower>();
        AddAbility<Clone>();
        AddAbility<AoeSpell>();
    }
}
