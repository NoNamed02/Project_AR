using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerModule
{
    public class PlayerMove : MonoBehaviour
    {
        private Vector2 moveInput;
        private float moveSpeed = 5f;
        private Coroutine moveCoroutine;

        private void OnEnable()
        {
            var playerInput = GetComponent<PlayerInput>();
            playerInput.actions["Move"].performed += OnMove;
            playerInput.actions["Move"].canceled += OnMoveCanceled;
        }

        private void OnDisable()
        {
            var playerInput = GetComponent<PlayerInput>();
            playerInput.actions["Move"].performed -= OnMove;
            playerInput.actions["Move"].canceled -= OnMoveCanceled;
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();

            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(MovePlayer());
            }
        }
        private void OnJump(InputAction.CallbackContext context)
        {

        }
        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            moveInput = Vector2.zero;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }

        private IEnumerator MovePlayer()
        {
            while (true)
            {
                Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
                transform.Translate(movement, Space.World);
                yield return null;  // 다음 프레임까지 대기
            }
        }
    }
}

