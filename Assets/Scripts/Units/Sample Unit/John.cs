using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class John : Unit
{
    // Start is called before the first frame update
    private void Awake()
    {
        Initialize(800, 900, 30, 2, 10);
        abilities.Add(gameObject.AddComponent<Empower>());
        abilities.Add(gameObject.AddComponent<ShockingOrb>());
    }
}
