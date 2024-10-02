using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PlayerBaseState
{
    private PlayerController controller;

    protected Player _player { get; private set; }
    
    public PlayerBaseState(Player player)
    {
        _player = player;
    }

    protected PlayerBaseState(PlayerController controller)
    {
        this.controller = controller;
    }

    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnFixedUpdateState();
    public abstract void OnExitState();
    //public abstract void OnStateEnter();
    //public abstract void OnStateUpdate();
    //public abstract void OnStateExit();
}
