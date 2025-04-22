using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] Unit unit;

    public void SetUnit(Unit unit)
    {
        this.unit = unit;
    }

    public Unit GetUnit() { return unit; }
}
