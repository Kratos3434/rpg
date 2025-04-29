using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = true;
    public LayerMask clickableLayers;
    private Unit unit;
    [SerializeField] MyPathFinder pf;
    int index = 0;

    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    private void FixedUpdate()
    {
        Vector2 target = pf.GetPath()[index];
        if (isMoving)
        {

            if (!unit.IsStunned())
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, target, unit.GetMovementSpeed() * Time.deltaTime);
                transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
            } else
            {
                Stop();
            }
        }

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            index++;
            if (index >= pf.GetPath().Count)
            {
                isMoving = false;
                index = 0;
                Debug.Log("Target Reached");
            }
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
