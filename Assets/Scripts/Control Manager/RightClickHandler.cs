using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickHandler : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] MainController mainController;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = 0;

                mainController.GetUnit().GetMovement().Move(targetPosition);
            }
        }
    }
}
