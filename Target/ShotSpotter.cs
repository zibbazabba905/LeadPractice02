using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSpotter : MonoBehaviour
{
    public GameObject HitMarker;
    private Vector3 UpdateShot;
    private float BlinkStart;
    private float BlinkDuration = 1f;

    void Update()
    {
        transform.LookAt(Vector3.zero);
        BlinkCheck();
    }

    public void MarkShot(Vector3 HitLocation)
    {
        HitMarker.transform.position = HitLocation;
        BlinkStart = Time.time;
        //StartCoroutine(Blink());
    }

    private void BlinkCheck()
    {
        if (Time.time < BlinkStart + BlinkDuration)
            HitMarker.SetActive(true);
        else
            HitMarker.SetActive(false);
    }
}
