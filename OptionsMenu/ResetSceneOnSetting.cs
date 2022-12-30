using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSceneOnSetting : MonoBehaviour
{
    Slider Slider;
    Toggle Toggle;

    private void Awake()
    {
        if(TryGetComponent<Toggle>(out Toggle))
        {
            Toggle.onValueChanged.AddListener(TestB);
        }
        else if(TryGetComponent<Slider>(out Slider))
        {
            Slider.onValueChanged.AddListener(TestF);
        }
    }
    private void TestB(bool UNUSED)
    {
        TargetManager.Instance.ResetTargets();
    }
    private void TestF(float UNUSED)
    {
        TargetManager.Instance.ResetTargets();
    }

}
