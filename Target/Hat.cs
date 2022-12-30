using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour, IShootable
    //NOTE Hats do not get reset after shot
{
    public Transform HatResetPoint;
    private Rigidbody rb;
    private bool Knocked = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void GetShot(Vector3 force, Vector3 location)
    {
        Knocked = true;
        rb.isKinematic = false;
        rb.AddForceAtPosition(force, location, ForceMode.Force);
        transform.parent = null;
    }
    public void ResetHat()
    {
        if (Knocked)
        {
            rb.isKinematic = true;
            transform.rotation = HatResetPoint.rotation;
            transform.position = HatResetPoint.position;
            transform.parent = HatResetPoint.parent;
        }
    }
}
