using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.GetComponent<TargetScript>().InCover = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            other.GetComponent<TargetScript>().InCover = false;
        }
    }
}
