using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This is the base class that all playable and non-playable units derive from
/// </summary>
public abstract class Unit : MonoBehaviour
{
    protected float maxHealth { get; set; }
    protected float health { get; set; }
    protected float maxMana { get; set; }
    protected float mana { get; set; }
    protected float baseDamage { get; set; }
    protected float bonusDamage { get; set; }
    protected float baseMovementSpeed { get; set; }
    protected float bonusMovementSpeed { get; set; }
    protected float baseHealthRegen { get; set; }
    protected float bonusHealthRegen { get; set; }
    protected float baseManaRegen { get; set;}
    protected float bonusManaRegen { get; set;}
    protected float baseAttackSpeed { get; set; }
    protected float bonusAttackSpeed { get; set;  }

    protected float attackRange = 3.39f;

    protected bool isMelee { get; set; }

    protected bool canAttack = true;

    protected bool isStunned = false;

    protected float stunDuration;

    private float stunDurationTimer = 0f;

    protected Unit targetUnit { get; set; }

    private UnitMovement movement;

    [SerializeField] HealthBar healthBar;
    [SerializeField] ManaBar manaBar;

    protected List<Ability> abilities = new List<Ability>(4);

    /// <summary>
    /// Attack modifiers are always cleared after every attack if it contains at least one
    /// </summary>
    protected List<Ability> attackModifiers = new List<Ability>();

    protected List<Debuff> debuffs = new List<Debuff>();

    protected Ability channeledAbility;

    private float healthRegenTimer = 0f;

    private float manaRegenTimer = 0f;

    [SerializeField] StatusBar statusBar;


    public void Initialize(float maxHealth, float maxMana, float baseDamage, float baseMovementSpeed = 2f, float baseHealthRegen = 1f, float baseManaRegen = 1.5f, float baseAttackSpeed = 5f)
    {
        this.maxHealth = maxHealth;
        this.maxMana = maxMana;
        this.baseDamage = baseDamage;
        this.baseMovementSpeed = baseMovementSpeed;
        this.baseHealthRegen = baseHealthRegen;
        this.baseManaRegen = baseManaRegen;
        this.baseAttackSpeed = baseAttackSpeed;
        health = this.maxHealth;
        mana = this.maxMana;
        bonusAttackSpeed = 0f;
        bonusDamage = 0f;
        bonusHealthRegen = 0f;
        bonusManaRegen = 0f;
        bonusMovementSpeed = 2f;
        manaBar.SetMaxMana(maxMana);
        healthBar.SetMaxHealth(maxHealth);
        isMelee = false;
    }

    public float GetDamage()
    {
        return baseDamage + bonusDamage;
    }

    public float GetMovementSpeed()
    {
        return baseMovementSpeed + bonusMovementSpeed;
    }

    public float GetHealthRegen()
    {
        return baseHealthRegen + bonusHealthRegen;
    }

    public float GetManaRegen()
    {
        return baseManaRegen + bonusManaRegen;
    }

    public float GetAttackSpeed()
    {
        return baseAttackSpeed + bonusAttackSpeed;
    }

    public void SetChanneledAbility(Ability channeledAbility)
    {
        this.channeledAbility = channeledAbility;
    }

    public Ability GetChanneledAbility()
    {
        return channeledAbility;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
    }

    public void SetBonusHealthRegen(float bonusHealthRegen)
    {
        this.bonusHealthRegen = bonusHealthRegen;
    }

    public float GetBonusHealthRegen()
    {
        return bonusHealthRegen;
    }

    public bool CanAttack() { return canAttack; }

    public float GetMana() { return mana;}

    public float GetMaxMana() { return maxMana;}

    public float GetBaseDamage() { return baseDamage; }

    public float GetBonusDamage()
    {
        return bonusDamage;
    }

    public Ability GetAbility(int index)
    {
        return (index > abilities.Count - 1 ? null : abilities[index]);
    }

    public List<Ability> GetAbilities() { 
        return abilities;
    }

    public void AddAbility<T>() where T : Ability
    {
        if (gameObject.GetComponent<T>() == null) {
            abilities.Add(gameObject.AddComponent<T>());
        } else
        {
            abilities.Add(gameObject.GetComponent<T>());
        }
    }

    public void SetMana(float mana) { this.mana = mana; }

    public void RemoveAllDebuff()
    {
        if (debuffs.Count > 0)
        {
            foreach (Debuff debuff in debuffs)
            {
                debuff.Dispel();
            }
            debuffs.Clear();
        }
    }

    public void SetBonusDamage(float bonusDamage) {  this.bonusDamage = bonusDamage; }

    public void SetBonusMovementSpeed(float bonusMovementSpeed)
    {
        this.bonusMovementSpeed = bonusMovementSpeed;
    }

    public float GetBonusMovementSpeed() { return bonusMovementSpeed; }

    public UnitMovement GetMovement()
    {
        return movement;
    }

    public float GetAttackRange() { return attackRange; }

    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public void RemoveAttackModifier(Ability ability)
    {
        attackModifiers.Remove(ability);
    }

    public void AddAttackModifier(Ability modifier)
    {
        attackModifiers.Add(modifier);
    }

    public void RemoveDebuff(Debuff debuff)
    {
        debuffs.Remove(debuff);
    }

    public void Stun(float duration)
    {
        if (duration > stunDurationTimer)
        {
            stunDuration = duration;
            stunDurationTimer = 0f;
            isStunned = true;
        }
    }

    public void UnStun()
    {
        stunDuration = 0f;
        stunDurationTimer = 0f;
        isStunned = false;
        statusBar.Deactivate();
    }

    public void CreateClone()
    {
        GameObject clone = Instantiate(gameObject);
        clone.name = $"{gameObject.name} Clone";
        clone.transform.position = transform.position;
    }

    /// <summary>
    /// This is called every after attack
    /// </summary>
    public void RemoveAttackModifiers()
    {
        if (attackModifiers.Count > 0)
        {
            foreach (Ability attackModifier in attackModifiers)
            {
                attackModifier.Dispel();
            }
            attackModifiers.Clear();
        }
    }

    public void AddDebuff(Debuff debuff)
    {
        debuffs.Add(debuff);
    }

    public bool IsMelee()
    {
        return isMelee;
    }
    public void SetTargetUnit(Unit targetUnit) { this.targetUnit = targetUnit; }

    public List<Ability> GetAttackModifiers() { return attackModifiers; }

    public void TakeDamage(Damage damage)
    {
        switch (damage.getType())
        {
            case Damage.Type.Physical: 
                health -= damage.getDamage();
                break;
            case Damage.Type.Magical:
                health -= damage.getDamage();
                break;
            case Damage.Type.Pure: 
                break;
        }
        Debug.Log($"I took {(int)damage.getDamage()} Damage");
        healthBar.SetHealth(health);
    }

    public ManaBar GetManaBar() { return manaBar; }

    public HealthBar GetHealthBar() { return healthBar; }

    private void RegenerateHealth()
    {
        healthRegenTimer += Time.deltaTime;

        if (healthRegenTimer >= (1f / GetHealthRegen()))
        {
            if (health >= maxHealth)
            {
                health = maxHealth;
            }
            else if (health < maxHealth)
            {
                //SetHealth(GetHealth() + 1f);
                health += 1f;
            }
            healthRegenTimer = 0f;
        }
        healthBar.SetHealth(health);
    }

    private void RegenerateMana()
    {
        manaRegenTimer += Time.deltaTime;

        if (manaRegenTimer >= (1f / GetManaRegen()))
        {
            if (mana >= maxMana)
            {
                mana = maxMana;
            } else if (mana < maxMana)
            {
                mana += 1f;
            }
            manaRegenTimer = 0f;
        }

        manaBar.SetMana(mana);
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    public void SetIsStunned(bool isStunned)
    {
        this.isStunned = isStunned;
    }

    private void Start()
    {
        movement = GetComponent<UnitMovement>();
        statusBar.Deactivate();
    }

    private void Update()
    {
        RegenerateHealth();
        RegenerateMana();

        if (isStunned)
        {
            if (!statusBar.IsActive())
            {
                statusBar.Activate();
            } else
            {
                statusBar.SetValues(stunDuration, stunDuration - stunDurationTimer);
            }

            stunDurationTimer += Time.deltaTime;

            if (stunDurationTimer >= stunDuration)
            {
                UnStun();
            }
        }


    }

}
