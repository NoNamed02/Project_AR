using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public const float CONVERT_UNIT_VALUE = 0.01f;
    public const float DEFAULT_CONVERT_MOVESPEED = 3f;
    public const float DEFAULT_ANIMATION_PLAYSPEED = 0.9f;
    private int hashMoveAnimation;

    public PlayerMoveState(PlayerController controller) : base(controller)
    {
        hashMoveAnimation = Animator.StringToHash("Velocity");
    }



    public override void OnEnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnFixedUpdateState()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdateState()
    {
        throw new System.NotImplementedException();
    }

    //public PlayerMoveState(Player player) : base(player)
    //{

    //}

    //public override void OnStateEnter()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override void OnStateExit()
    //{
    //    throw new System.NotImplementedException();
    //}

    //public override void OnStateUpdate()
    //{
    //    throw new System.NotImplementedException();
    //}
}
