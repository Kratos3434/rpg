using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void Update()
    {
        try
        {
            if (unit.GetAbilities().Count == 0) throw new System.Exception("No Abilities Yet");

            if (Input.GetKeyDown(KeyCode.Q))
            {
                DisplayMessage(unit, "Q");
                if (unit.GetAbility(0))
                {
                    unit.GetAbility(0).Activate();
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                DisplayMessage(unit, "W");
                if (unit.GetAbility(1))
                {
                    unit.GetAbility(1).Activate();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                DisplayMessage(unit, "E");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                DisplayMessage(unit, "R");
            }
        } catch (System.Exception e)
        {
            Debug.Log(e.Message);
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
