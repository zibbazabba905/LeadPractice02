using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    /*
    private TargetScript TargetScript;
    private CharacterController CC;


    void Awake()
    {
        TargetScript = GetComponent<TargetScript>();
        CC = GetComponent<CharacterController>();
    }

    private void HandleMovement()
    {
        //if target hits left or right wall, change directions

        //if (transform.position.x < -PropManager.Instance.RangeHalfWidth+1 || transform.position.x > PropManager.Instance.RangeHalfWidth -1)
        if (TargetNearGoal(transform.position, TargetScript.GoalPosition))
            FlipDirection();
        CC.Move(TowardsGoalPosition() * TargetScript.MovementSpeed * Time.deltaTime);
        //move target left or right by current movement speed by frame time
        //CC.Move((MovingLeft ? Vector3.left : Vector3.right) * MovementSpeed * Time.deltaTime);
    }

    private Vector3 TowardsGoalPosition()
    {
        Debug.DrawRay(transform.position, TargetScript.GoalPosition - transform.position);
        return (TargetScript.GoalPosition - transform.position).normalized;
    }



    public void SetTarget()
    {
        SetTargetSpeed();
        TargetScript.MovingLeft = RandomBool();

        float x = Random.Range(-PropManager.Instance.RangeHalfWidth, PropManager.Instance.RangeHalfWidth);
        float y = 1f; //Figure out this, height
        float z = PropManager.Instance.ActiveRow;

        transform.position = new Vector3(x,y,z);
        // Update the GameObject's position using the CharacterController.Move method.
        //CC.Move((MovingLeft ? Vector3.left : Vector3.right) * MovementSpeed * Time.deltaTime);
    }

    private float MoveType()
    {
        float Multiplier = 0;
        if (TargetManager.Instance.CanWalk)
            Multiplier = (RandomBool() ? 0f : 1f);
        if (TargetManager.Instance.CanRun)
            Multiplier = (RandomBool() ? 0f : 2f);
        if (TargetManager.Instance.CanWalk && PropManager.Instance.CanRun)
            Multiplier = (int)Random.Range(0, 3);

        if (Multiplier >= 2)
            transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        else
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);


        return TargetScript.BaseSpeed * Multiplier;
    }
    private bool RandomBool()
    {
        return (Random.value >= 0.5);
    }
    public void FlipDirection()
    {
        TargetScript.MovingLeft = !TargetScript.MovingLeft;
        TargetScript.GetNewGoalPosition();
    }
    public void TargetHit()
    {
        if (!TargetManager.Instance.RandomSpawn)
        {
            FlipDirection();
            TargetScript.MovementSpeed = MoveType();
            return;
        }
        SetTarget();
    }
    public void SetTargetSpeed()
    {
        TargetScript.MovementSpeed = MoveType();
    }

    public bool TargetNearGoal(Vector3 target, Vector3 goal)
    {
        return Vector3.Distance(goal,target) < 0.01;
    }
    */
}
