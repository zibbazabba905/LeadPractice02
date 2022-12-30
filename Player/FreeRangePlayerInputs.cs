using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRangePlayerInputs : MonoBehaviour
{
    private PlayerInput PlayerInput;
    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        if (PlayerInput.GetKeyInputDown(KeyCode.R))
            TargetManager.Instance.ResetTargets();
        if (PlayerInput.GetKeyInputDown(KeyCode.E))
            PropManager.Instance.IncreaseDistance();
        if (PlayerInput.GetKeyInputDown(KeyCode.Q))
            PropManager.Instance.DecreaseDistance();
    }
}
