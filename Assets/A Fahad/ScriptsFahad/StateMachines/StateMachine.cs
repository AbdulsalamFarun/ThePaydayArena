using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private StateFahad currentState;

    public void SwitchState(StateFahad newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
