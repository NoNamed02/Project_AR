using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    // PlayerStateManager를 만들어야할까?

    private Vector2 moveInput;
    private float moveSpeed = 3f;
    private Coroutine moveCoroutine;

    private float jumpInput;
    private float jumpTimer=0.2f;
    private float jumpTime = 0;
    private int jumpCount = 0;
    private int jumpChance = 1;
    private Coroutine jumpCoroutine;

    private float dashInput;
    private float dashTimer = 0.25f;
    private float dashTime = 0;
    private int dashCount = 0;
    private int dashChance = 1;
    private Vector2 dashDir = Vector2.right;
    private Coroutine dashCoroutine;
    private bool isDash = false;
    Rigidbody rigid;

    private void Start()
    {
        rigid=GetComponent<Rigidbody>();
    }

    #region InputEnable
    private void OnEnable()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed += OnMove;
        playerInput.actions["Move"].canceled += OnMoveCanceled;
        playerInput.actions["Jump"].performed += OnJump;
        playerInput.actions["Jump"].canceled += OnJumpCanceled;
        playerInput.actions["Dash"].performed += OnDash;
        //playerInput.onActionTriggered += PlayerInput_onActionTriggered;
    }

    private void OnDisable()
    {
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions["Move"].performed -= OnMove;
        playerInput.actions["Move"].canceled -= OnMoveCanceled;
    }
    #endregion
    #region Move
    // PlayerAction.Move : When start Input Signal
    private void OnMove(InputAction.CallbackContext context)
    {
        if (isDash == false)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        dashDir = moveInput;
        if (moveCoroutine == null)
        {
            moveCoroutine = StartCoroutine(MovePlayer());
        }
    }

    // PlayerAction.Move : When end Input Signal
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
    }

    // PlayerAction.Move : During in Input "Move"
    // This is Why Player move
    private IEnumerator MovePlayer()
    {
        while (true)
        {
            // Option 1. Move with Translate

            Vector3 movement = new Vector3(moveInput.x * moveSpeed * Time.deltaTime, 0, 0);
            transform.Translate(movement, Space.World);

            // Option 2. Move with Velocity

            //rigid.velocity=new Vector3(moveInput.x*moveSpeed,rigid.velocity.y, 0);


            yield return null;  // 다음 프레임까지 대기
        }
    }
    #endregion
    #region Jump
    // PlayerAction.Jump : When start Input Signal
    private void OnJump(InputAction.CallbackContext context)
    {
        //if(context.performed)
        //{
        //    Debug.Log("Jump "+ context.phase);
        //    rigid.AddForce(Vector3.up * 50);
        //}
        if (jumpTime > jumpTimer)
        {
            jumpInput = 0;
        }

        if (jumpInput == 0&&jumpTime<jumpTimer)
        {
            jumpInput = context.ReadValue<float>();
        }

        //Debug.Log(jumpInput);
        //Debug.Log("JumpCount = " + jumpCount);

        if (jumpCoroutine == null && jumpCount <= jumpChance)
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0, 0);
            jumpTime = 0;
            jumpCount++;
            jumpCoroutine = StartCoroutine(JumpPlayer());
        }
    }


    // PlayerAction.Jump : When end Input Signal
    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpInput = 0;
        jumpTime = 0;
        if (jumpCoroutine != null)
        {
            StopCoroutine(jumpCoroutine);
            jumpCoroutine = null;
        }
    }


    // PlayerAction.Jump : During in Input "Jump"
    // This is Why Player jump
    private IEnumerator JumpPlayer()
    {
        while (true)
        {
            if (isDash==false && jumpTime <= jumpTimer)
            {
                jumpTime += Time.deltaTime;
                transform.Translate(Vector2.up * 0.15f, Space.World);
            }
            else
            {
                transform.Translate(Vector2.up*rigid.GetAccumulatedForce(), Space.World);
            }

            yield return null;
        }
    }
    #endregion
    #region Dash
    // PlayerAction.Dash : When start Input Signal
    private void OnDash(InputAction.CallbackContext context)
    {
        if (isDash == false)
        {
            dashInput = context.ReadValue<float>();
            isDash = true;
        }

        if (isDash)
        {
            dashTime = 0;
            dashCoroutine = StartCoroutine(DashPlayer());
        }

        Debug.Log("Dash 클릭됨");
    }

    // PlayerAction.Dash : During in Dash
    // This is Why Player Dash
    private IEnumerator DashPlayer()
    {
        while (true)
        {
            if (dashTime <= dashTimer)
            {
                rigid.useGravity = false;
                dashTime += Time.deltaTime;
                //transform.Translate(dashDir * 1.1f, Space.World);
                rigid.velocity = dashDir * 15f;
                //Debug.Log("리지드 벨로시티"+rigid.velocity);
                //Debug.Log(dashTime);
                Debug.Log(isDash);
            }
            else
            {
                StopCoroutine(dashCoroutine);
                rigid.velocity = dashDir * 0;
                dashInput= 0;
                rigid.useGravity = true;
                dashCoroutine = null;
                isDash = false;
            }

            yield return null;
        }
    }

    #endregion

    private void OnCollisionStay(Collision collision)
    {
        jumpCount = 0;
    }
}
