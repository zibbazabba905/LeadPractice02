using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public GameObject CameraRig;

    private float _Sens;
    public float Sens
    {
        get { return _Sens; }
        set { _Sens = value; }
    }

    private float RigVerticalAngle;

    void LateUpdate()
    {
        //FIX move inputs to player input
        if (!GameManager.Instance.Paused)
        {
            //horizontal rotation
            //transform.Rotate(new Vector3(0f, Input.GetAxisRaw("Mouse X") * Sens, 0f) , Space.Self);            
            transform.Rotate(new Vector3(0f, (Input.GetAxisRaw("Mouse X") * Sens), 0f) , Space.Self);            

            //vertical rotation
            //vert input to rig vertical angle
            RigVerticalAngle -= (Input.GetAxisRaw("Mouse Y") * Sens);
            //Limit the vertical angle min/max
            RigVerticalAngle = Mathf.Clamp(RigVerticalAngle, -85f, 85f);
            //Apply vert angle as local rotation to transform "along it's right axis?"
            CameraRig.transform.localEulerAngles = new Vector3(RigVerticalAngle, 0, 0);            
        }
        //sway should go into here
    }
}

