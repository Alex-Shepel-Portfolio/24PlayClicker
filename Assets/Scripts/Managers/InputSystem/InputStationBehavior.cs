using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputStationBehavior : MonoSingleton<InputStationBehavior>, IStationstateSwitcher
{
    //[SerializeField, Range(-0.1f,0.5f)] private float updateStep = -0.1f;
    private List<InputState> allSate;
    private InputState currentState;

    private Coroutine updateCorutine;
    
    public void Init(InputStateType stateForStart)
    {
        InitStates(stateForStart);
    }

    private void InitStates(InputStateType stateForStart)
    {
        allSate = new List<InputState>()
        { 
            new InputClickerState(this),
            new InputSwipeMoveState(this),
            new InputMinZoneMoveState(this)
        };
        currentState = GetStateWithType(stateForStart);
        currentState.Start();
        StartUpdate();
    }

    private InputState GetStateWithType(InputStateType stateForStart)
    {
        InputState stateToSet = null;
        switch (stateForStart)
        {
            case InputStateType.ClickerState:
                stateToSet = allSate.FirstOrDefault(s => s is InputClickerState);
                break;
            case InputStateType.SwipeMoveState:
                stateToSet =allSate.FirstOrDefault(s => s is InputSwipeMoveState);
                break;
            case InputStateType.MinZoneMoveState:
                stateToSet =allSate.FirstOrDefault(s => s is InputMinZoneMoveState);
                break;
        }

        return stateToSet;
    }

    public void SwitchState(InputStateType stateType)
    {
        currentState.GetOtherState(stateType);
    }

    private void StartUpdate()
    {
        if (updateCorutine != null) {return; }
        updateCorutine = StartCoroutine(InputUpdate());
    }
    private void StopUpdate()
    {
        if (updateCorutine == null) {return; }
        StopCoroutine(updateCorutine);
        updateCorutine = null;
    }
    IEnumerator InputUpdate()
    {
        while (currentState.IsNeedUpdate)
        {
            currentState.Update();
            yield return null;
        }
        updateCorutine = null;
    }

    public void SwitchState<T>() where T : State
    {
        StopUpdate();
        currentState.Stop();
        var state = allSate.FirstOrDefault(s => s is T);
        state.Start();
        currentState = state;
        StartUpdate();
    }
}
