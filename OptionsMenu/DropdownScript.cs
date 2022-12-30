using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownScript : MonoBehaviour
{
    public Dropdown DD;
    void Awake()
    {
        DD = GetComponent<Dropdown>();
        Invoke("InitValue", 0.01f); //do init while loop
    }

    private void InitValue()
    {
        DD.onValueChanged.Invoke(DD.value);
    }
}
