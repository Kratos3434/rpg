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
            DisplayMessage(unit, "Q");
            if (unit.GetAbility(0))
            {
                unit.GetAbility(0).Activate();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            DisplayMessage(unit, "W");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            DisplayMessage(unit, "E");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            DisplayMessage(unit, "R");
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
