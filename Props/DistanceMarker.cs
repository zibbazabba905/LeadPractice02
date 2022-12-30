using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceMarker : MonoBehaviour
{
    private Text SignText;
    private void Awake()
    {
        SignText = GetComponentInChildren<Text>();
        SignText.text = Mathf.RoundToInt(transform.position.z +1).ToString();
    }
}
