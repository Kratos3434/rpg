using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickHandler : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] MainController mainController;
    [SerializeField] HudDisplayManager hudDisplayManager;
    [SerializeField] LayerMask clickableLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (!mainController.GetUnit().IsStunned())
                {
                    targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    targetPosition.z = 0;
                    //mainController.GetUnit().GetMovement().Stop();
                    //mainController.GetUnit().GetMovement().SetPathFinder(new MyPathFinder(mainController.GetUnit().transform.position, targetPosition));
                    mainController.GetUnit().GetMovement().Move(targetPosition);

                    if (hudDisplayManager.GetUnit() != mainController.GetUnit())
                    {
                        hudDisplayManager.SetUnit(mainController.GetUnit());
                    }

                    RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero, Mathf.Infinity, clickableLayers);

                    if (hit.collider)
                    {
                        Unit unit = hit.collider.GetComponent<Unit>();

                        if (unit && unit != mainController.GetUnit())
                        {
                            mainController.GetUnit().SetTargetUnit(unit);
                        }
                    }
                    else
                    {
                        mainController.GetUnit().SetTargetUnit(null);
                    }
                } else
                {
                    mainController.GetUnit().SetTargetUnit(null);
                    DisplayManager.errorMessage = "Stunned";
                }
            }
        }
    }
}
