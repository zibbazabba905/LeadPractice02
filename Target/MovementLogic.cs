using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CCMovment))]
public class MovementLogic : MonoBehaviour
{
    private CCMovment CCMovement;
    private TargetScript TargetScript;

    public List<Vector3> GoalPoints = new List<Vector3>();

    private bool FromCover { get; set; } = false;
    private float CoverTimerFireTime { get; set; } = -1f;
    private bool TwitchTimerSet { get; set; }
    private float TwitchTimerFireTime { get; set; } = 1f;
    public enum MoveState { Wait, Move, NewPos }
    public MoveState CurrentMoveState = MoveState.Wait;


    private void Awake()
    {
        CCMovement = GetComponent<CCMovment>();
        TargetScript = GetComponent<TargetScript>();
        PropManager.Instance.CoverCountResetEvent.AddListener(BuildGoalPointList);
        BuildGoalPointList();
        PickRandomDirection();
        SetSpeedAndUpdateCC();
    }

    private void Update()
    {
        if (CurrentMoveState == MoveState.Move)
        {
            if(TwitchTimeActive())
            {
                if (!TwitchTimerSet)
                {
                    float newTime = Random.Range(0.5f, TargetManager.Instance.TwitchTime);
                    TwitchTimerFireTime = Time.time + newTime;
                    TwitchTimerSet = true;
                }

                if (TimerEnd(TwitchTimerFireTime))
                {
                    TwitchTimerSet = false;
                    FlipDirection();
                    CurrentMoveState = MoveState.NewPos;
                }
            }

            if (!CCMovement.MovementComplete())
            {
                if (FromCover)
                {
                    if (TimerEnd(CoverTimerFireTime))
                    {
                        FlipDirection();
                        CurrentMoveState = MoveState.NewPos;
                        FromCover = false;
                        return;
                    }
                }
                return;
            }

            if (!TargetScript.InCover)
            {
                CurrentMoveState = MoveState.NewPos;
                return;
            }
            StartWaitTimer(1f);
            FromCover = true;
            CurrentMoveState = MoveState.Wait;
            return;
        }

        if (CurrentMoveState == MoveState.Wait)
        {
            if (!TimerEnd(CoverTimerFireTime))
                return;

            PickRandomDirection();
            SetSpeed();
            StartWaitTimer(Random.Range(0.5f,1f));

            CurrentMoveState = MoveState.NewPos;
            return;
        }
        if (CurrentMoveState == MoveState.NewPos)
        {
            UpdateCCPos();
            CurrentMoveState = MoveState.Move;
            return;
        }
    }

    private void StartWaitTimer(float time)
    {
        //CoverTimerSet = true;
        CoverTimerFireTime = Time.time + time;
    }
    private bool TimerEnd(float timerTime)
    {
        return (Time.time > timerTime);
    }

    public void PickRandomDirection()
    {
        TargetScript.MovingLeft = RandomBool();
    }

    public void FlipAndSetAndUpdate()
    {
        FlipDirection();
        SetSpeedAndUpdateCC();
    }

    public void SetSpeedAndUpdateCC()
    {
        SetSpeed();
        UpdateCCPos();
    }

    public void UpdateCCPos()
    {
        CCMovement.GoTo(PickNewGoal(), TargetScript.MovementSpeed);
    }

    private void SetSpeed()
    {
        float Multiplier = 0;
        if (TargetManager.Instance.CanWalk)
            Multiplier = (RandomBool() ? 0f : 1f);
        if (TargetManager.Instance.CanRun)
            Multiplier = (RandomBool() ? 0f : 2f);
        if (TargetManager.Instance.CanWalk && TargetManager.Instance.CanRun)
            Multiplier = (int)Random.Range(0, 3);

        if (TargetScript.InCover || !TargetManager.Instance.RandomSpawn)
        {
            if (Multiplier < 1)
                Multiplier = (int)Random.Range(1, 3);
        }

        if (Multiplier >= 2)
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        TargetScript.MovementSpeed = Multiplier * TargetScript.BaseSpeed;
    }

    private Vector3 PickNewGoal()
    {
        //find closest cover point
        Vector3 closest = Vector3.zero;
        float minDist = Mathf.Infinity;
        Vector3 currentpos = transform.position;
        foreach (var point in GoalPoints)
        {
            //skip point if not in direction of travel
            if (!InTravelDirection(point))
                continue;

            //get distance between object and point of interest
            float dist = Vector3.Distance(point, currentpos);

            //if smaller than shortest distance || not too close, update data
            if (dist < minDist && dist > TargetScript.ExclusionDistance)
            {
                closest = point;
                minDist = dist;
            }
        }

        //if on edge, flip around and try again
        if (closest != Vector3.zero)
            return closest;
        FlipDirection();
        return PickNewGoal();
    }
    private bool InTravelDirection(Vector3 pointOfInterest)
    {
        //is point stage left/right of object?
        float pointX = pointOfInterest.x;
        float objectX = transform.position.x;
        return TargetScript.MovingLeft == (objectX > pointX);
    }

    private void FlipDirection()
    {
        TargetScript.MovingLeft = !TargetScript.MovingLeft;
    }

    private bool RandomBool()
    {
        return (Random.value >= 0.5);
    }
    private void BuildGoalPointList()
    {
        GoalPoints.Clear();
        GoalPoints.Add(PropManager.Instance.GetEdgeActiveRow("Left"));
        GoalPoints.Add(PropManager.Instance.GetEdgeActiveRow("Right"));

        foreach (var panel in PropManager.Instance.ActiveCoverPanels)
        {
            GoalPoints.Add(panel.transform.position);
        }
    }
    private bool TwitchTimeActive()
    {
        return TargetManager.Instance.TwitchTime > 0;
    }
}