using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectWithMouse : MonoBehaviour
{
    private Vector3 mOffset;
    float mZCoord;
    float mXCoord;

    void OnMouseDown(){
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mXCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    void OnMouseDrag(){
        transform.position = GetMouseAsWorldPoint() + mOffset;
    }

    private Vector3 GetMouseAsWorldPoint(){
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        mousePoint.x = mXCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
