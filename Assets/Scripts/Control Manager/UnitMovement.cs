using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = false;
    public LayerMask clickableLayers;
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, unit.GetMovementSpeed() * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void Move(Vector3 targetPosition)
    {
        isMoving = true;
        this.targetPosition = targetPosition;
    }

    public void Stop()
    {
        isMoving = false;
    }
}
