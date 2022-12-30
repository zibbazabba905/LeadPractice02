using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPart : MonoBehaviour, IShootable
{
    TargetScript parent;
    private Renderer render;
    private float StartBlink;
    private float BlinkDuration = 0.1f;
    private Material OriginalColor;

    void Awake()
    {
        parent = GetComponentInParent<TargetScript>();
        render = GetComponent<Renderer>();
        OriginalColor = render.material;
    }
    public void GetShot(Vector3 force, Vector3 location)
    {
        parent.GetHit(gameObject.name);
        StartBlink = Time.time;
    }
    void Update()
    {
        if (Time.time < StartBlink + BlinkDuration)
            render.material = parent.HitColor;
        else
            render.material = OriginalColor;
    }
}
