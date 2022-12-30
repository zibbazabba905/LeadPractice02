using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntView : MonoBehaviour
{
    public Camera FpsCam;
    public GameObject Crosshair;

    private bool _lowered;
    public bool Lowered
    {
        get { return _lowered; }
        set { _lowered = value;
            ViewSettings(); }
    }

    void Start()
    {
        Lowered = true;
        ViewSettings();
    }
    private void ViewSettings()
    {
        FpsCam.lensShift = Lowered ? new Vector2(0f, 0.085f) : new Vector2(0f, 0f);
        Crosshair.transform.position = Lowered ? new Vector3(HScrPosConv( 960f), VScrPosConv(432f), 0f) : new Vector3(HScrPosConv(960f), VScrPosConv(540f), 0f);
    }

    private float HScrPosConv(float px)
    {
        return (px / 1920) * Screen.width;
    }
    private float VScrPosConv(float px)
    {
        return (px / 1080) * Screen.height;
    }

}
