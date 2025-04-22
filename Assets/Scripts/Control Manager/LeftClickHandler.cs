using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftClickHandler : MonoBehaviour
{
    [SerializeField] MainController mainController;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] HudDisplayManager hudDisplayManager;
    private Vector3 targetPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = 0;

                RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero, Mathf.Infinity, clickableLayers);
                
                if (hit.collider && hit.collider != mainController.GetUnit().GetComponent<Collider2D>()) {
                    Unit unit = hit.collider.GetComponent<Unit>();
                    if (LayerMask.LayerToName(unit.gameObject.layer) == "Ally")
                    {
                        mainController.SetUnit(unit);
                    }
                    hudDisplayManager.SetUnit(unit);
                }
            }
        }
    }
}
