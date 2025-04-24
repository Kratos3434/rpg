using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ActivateAbility(0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ActivateAbility(1);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateAbility(2);

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ActivateAbility(3);
        }
    }

    private void ActivateAbility(int index)
    {
        try
        {
            if (unit.GetAbilities().Count == 0) throw new System.Exception("No Abilities Yet");
            if (index > unit.GetAbilities().Count - 1) throw new System.Exception("Ability Slot is Empty");

            unit.GetAbility(index).Activate();
        } catch (System.Exception e)
        {
            DisplayManager.errorMessage = e.Message;
        }
    }

    void DisplayMessage(Unit unit, string key)
    {
        Debug.Log($"{unit.name} pressed {key}");
    }

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public Unit GetUnit() { return unit; }

}
