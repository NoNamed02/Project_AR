using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //public PlayerStateMachine(PlayerBaseState initState)
    //{
    //    _curState=initState;

    //}

    //private PlayerBaseState _curState;

    //public void ChangeState(PlayerBaseState nextState)
    //{
    //    if (nextState == _curState)
    //        return;
    //    if (_curState != null)
    //        _curState.OnStateExit();

    //    _curState = nextState;
    //    _curState.OnStateEnter();
    //}

    //public void UpdateState()
    //{
    //    if(_curState != null)
    //        _curState.OnStateUpdate();
    //}

    #region #과거코드
    public PlayerBaseState CurrentState { get; private set; }
    private Dictionary<StateName, PlayerBaseState> states =
        new Dictionary<StateName, PlayerBaseState>();


    public PlayerStateMachine(StateName stateName, PlayerBaseState state)
    {

    }

    public PlayerBaseState AddState(StateName stateName, PlayerBaseState state)
    {
        if (!states.TryGetValue(stateName, out PlayerBaseState newState))
            return state;
        return null;
    }

    public void DeleteState(StateName removeStateName)
    {
        if (states.ContainsKey(removeStateName))
        {
            states.Remove(removeStateName);
        }
    }

    public void ChangeState(StateName nextStateName)
    {
        CurrentState?.OnExitState();
        if (states.TryGetValue(nextStateName, out PlayerBaseState newState))
        {
            CurrentState = newState;
        }
        CurrentState?.OnEnterState();
    }

    public void UpdateState()
    {
        CurrentState?.OnUpdateState();
    }

    public void FixedUpdateState()
    {
        CurrentState?.OnFixedUpdateState();
    }
    #endregion
}