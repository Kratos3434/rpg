using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseHover : MonoBehaviour
{
    private static Unit hoveredUnit;
    [SerializeField] LayerMask hoverableLayers;
    private static Vector3 targetPosition;

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;

            //Check if its an enemy
            RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero, Mathf.Infinity, hoverableLayers);

            if (hit.collider != null)
            {
                hoveredUnit = hit.collider.GetComponent<Unit>();
            }
            else
            {
                hoveredUnit = null;
            }
        }
    }

    public static Unit GetHoveredUnit() { return hoveredUnit; }

    public static Vector3 GetTargetPosition() {  return targetPosition; }


}
