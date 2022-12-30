using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensitivityLevels : MonoBehaviour
{
    //mouse sensitivity conversion with Hunt and Unity
    const float DOWNRATIO = 1.125f;
    const float HIPRATIO = 1.25f;
    const float ADSRATIO = 2.25f;


    private float _downSens = 1.5f;
    public float DownSens
    {
        get { return _downSens; }
        set { _downSens = RoundVal(value); }
    } 

    private float _hipSens = 1.0f; 
    public float HipSens
    {
        get { return _hipSens; }
        set { _hipSens = RoundVal(value); }
    }

    private float _adsSens = 0.6f;
    public float ADSSens
    {
        get { return _adsSens; }
        set { _adsSens = RoundVal(value); }
    }

    private float RoundVal(float value)
    {
        return Mathf.Round(value * 100) / 100;
    }
    
    private float HuntToUnitySensConversion(float ratio, float value)
    {
        return value / ratio;
    }

    //whatever theres probably easier ways of doing this
    public float GetDownSens()
    {
        return HuntToUnitySensConversion(DOWNRATIO, DownSens);
    }
    public float GetHipSens()
    {
        return HuntToUnitySensConversion(HIPRATIO, HipSens);
    }
    public float GetADSSens()
    {
        return HuntToUnitySensConversion(ADSRATIO, ADSSens);
    }
}
