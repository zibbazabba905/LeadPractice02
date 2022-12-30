using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private MovementLogic MovementLogic;
    private TargetDeath TargetDeath;

    public Hat Hat;
    public Material HitColor;

    public bool InCover { get; set; }
    public bool MovingLeft { get; set; }
    public float ExclusionDistance { get; set; } = 0.5f;
    public float MovementSpeed { get; set; } = 0f;

    public float BaseSpeed = 2.5f;
    
    void Awake()
    {
        MovementLogic = GetComponent<MovementLogic>();
        TargetDeath = GetComponent<TargetDeath>();
    }

    public void GetHit(string HitPart)
    {
        if (TargetManager.Instance.RandomSpawn)
        {
            KillTarget();
        }
        else
        {
            MovementLogic.FlipAndSetAndUpdate();
        }
    }
    public void KillTarget()
    {
        //NOTE: Hats are not reset with the targets
        TargetManager.Instance.ActiveTargetsList.Remove(this);
        TargetDeath.enabled = true;
    }
}