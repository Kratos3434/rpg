using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UnitMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private bool isMoving = false;
    public LayerMask clickableLayers;
    private Unit unit;
    //MyPathFinder pf;
    //int index = 0;

    private void Awake()
    {
        unit = GetComponent<Unit>();
        //pf = null;
    }

    private void FixedUpdate()
    {
        //if (pf == null) return;

        //if (pf != null)
        //{
        //    Vector2 target = pf.GetPath()[index];
        //    if (isMoving)
        //    {

        //        if (!unit.IsStunned())
        //        {
        //            Vector2 newPos = Vector2.MoveTowards(transform.position, target, unit.GetMovementSpeed() * Time.deltaTime);
        //            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        //        }
        //        else
        //        {
        //            Stop();
        //        }
        //    }

        //    if (Vector3.Distance(transform.position, target) < 0.1f)
        //    {
        //        index++;
        //        if (index >= pf.GetPath().Count)
        //        {
        //            isMoving = false;
        //            index = 0;
        //            pf = null;
        //            Debug.Log("Target Reached");
        //        }
        //    }
        //}

        if (isMoving)
        {

            if (!unit.IsStunned())
            {
                Vector2 newPos = Vector2.MoveTowards(transform.position, targetPosition, unit.GetMovementSpeed() * Time.deltaTime);
                transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
            }
            else
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

    //public void SetPathFinder(MyPathFinder pf)
    //{
    //    Debug.Log("Path finder set");
    //    this.pf = pf;
    //    isMoving = true;
    //}

    public void Stop()
    {
        //pf = null;
        isMoving = false;
        //index = 0;
    }

    public void Resmue()
    {
        isMoving = true;
    }

    public bool IsMoving() { return isMoving; }
}
