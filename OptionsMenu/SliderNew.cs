using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderNew : MonoBehaviour
{
    Text OptionText;
    Slider slider;
    public string SliderName;
    private void Awake()
    {
        OptionText = GetComponentInChildren<Text>();
        slider = GetComponent<Slider>();
    }
    public void UpdateSliderName(float value)
    {
        //because somehow this is called before Awake sometimes
        if (OptionText ==null)
            OptionText = GetComponentInChildren<Text>();

        OptionText.text = $"{SliderName} : {value}";
    }
}
