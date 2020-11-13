using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mouse", menuName =("Mouse/MouseInfo"))]
public class MouseInfo : ScriptableObject
{
    PlayerInputActions playerInputActions;
    Vector3 LookPosition;
    Vector2 mousePos;

    public void OnEnable()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Land.MousePos.performed += x => mousePos = x.ReadValue<Vector2>();
    }

    public Vector3 ReturnMousePos(Transform objectToRotate)
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(mousePos);

        RaycastHit hit;

        if (Physics.Raycast(mousePosition, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            Debug.DrawLine(mousePosition.origin, hit.point, Color.blue, 0.2f);
            LookPosition = new Vector3(hit.point.x, objectToRotate.transform.position.y, hit.point.z);
            return LookPosition;
        }
        else
        {
            return LookPosition;
        }
    }
}
