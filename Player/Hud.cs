using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public RawImage IronsightsHudElement;
    public Camera FPSCam;
    public GameObject ScreenAimPoint;
    public GameObject BulletSpawn;

    public Vector3 AimPointCalc()
    {
        Vector3 ap = ScreenAimPoint.transform.position;
        float w = Screen.width;
        float h = Screen.height;
        return new Vector3(ap.x / w, ap.y / h, 1f);
    }
}
