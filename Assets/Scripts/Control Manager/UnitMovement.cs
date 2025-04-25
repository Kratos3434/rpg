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

            if (!unit.IsStunned())
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, unit.GetMovementSpeed() * Time.deltaTime);
            } else
            {
                Stop();
            }
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMoving = false;
        }
    }

    public void Move(Vector3 targetPosition)
    {
        try
        {
            if (unit.IsStunned()) throw new System.Exception("Stunned");

            isMoving = true;
            this.targetPosition = targetPosition;
        } catch (System.Exception e)
        {
            DisplayManager.errorMessage = e.Message;
        }
    }

    public void Stop()
    {
        isMoving = false;
    }
}
