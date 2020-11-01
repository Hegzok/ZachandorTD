using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Mouse", menuName =("Mouse/MouseInfo"))]
public class MouseInfo : ScriptableObject
{
    Vector3 LookPosition;

    public Vector3 ReturnMousePos(Transform objectToRotate)
    {
        Ray mousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

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
