using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private bool _didAttackInput;

    void LateUpdate()
    {
        _didAttackInput = GetAttackInputHeld();
    }

    private bool CanProcessInputs()
    {
        return (!GameManager.Instance.Paused);
    }

    public Vector3 GetMovementInputs()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);//normalizes movement
        return move;
    }
    public bool GetSprintInput()
    {
        return Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && !Input.GetMouseButton(1);
    }
    public bool GetJumpInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool GetAttackInputHeld()
    {
        if (CanProcessInputs())
        {
            return Input.GetButton("Fire1");
        }
        return false;
    }

    public bool GetKeyInputDown(KeyCode key)
    {
        if (CanProcessInputs())
        {
            return Input.GetKeyDown(key);
        }
        return false;
    }
}
