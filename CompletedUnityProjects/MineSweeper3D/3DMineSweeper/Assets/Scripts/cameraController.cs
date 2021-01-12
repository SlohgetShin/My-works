using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    private float sensitivity;
    private float dampner;

    private float xRot;
    private float yRot;

    private float currY;
    private float currX;

    private float xVelRef;
    private float yVelRef;
    // Start is called before the first frame update
    void Start()
    {
        sensitivity = 5f;
        dampner = .3f;
    }

    // Camera follows the mouse
    void LateUpdate()
    {
        yRot += Input.GetAxis("Mouse X") * sensitivity;
        xRot -= Input.GetAxis("Mouse Y") * sensitivity;

        currX = Mathf.SmoothDamp(currX, xRot, ref xVelRef, dampner);
        currY = Mathf.SmoothDamp(currY, yRot, ref yVelRef, dampner);

        xRot = Mathf.Clamp(xRot, -85, 85);

        transform.rotation = Quaternion.Euler(currX, currY, 0);
    }

    public float getCurrY()
    {
        return currY;
    }

    public float getCurrX()
    {
        return currX;
    }
}
