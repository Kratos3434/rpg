using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : Unit
{
    private void Awake()
    {
        Initialize(500, 500, 50);
        abilities.Add(gameObject.AddComponent<Empower>());
        abilities.Add(gameObject.AddComponent<ShockingOrb>());
        abilities.Add(gameObject.AddComponent<Blink>());

    }
}
