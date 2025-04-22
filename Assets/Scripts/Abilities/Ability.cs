using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the base class of all spells/abilities used by every unit that has an ability
/// </summary>
public abstract class Ability : Spell
{
    protected int currentLevel = 0;
    protected int maxLevel;
    protected List<float> damage;
    protected List<float> duration;
    
}
