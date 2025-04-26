using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    public CinemachineFreeLook freeLookCamera;

    public Transform cam;
    public float playerMoveSpeed = 2.2f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        if(!AreMenusActive())
        {
            HandleMovement();
            UpdateMovementAnimation();
            freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
            freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            freeLookCamera.m_XAxis.m_InputAxisName = "";
            freeLookCamera.m_YAxis.m_InputAxisName = "";
            ResetMovementAnimation();
        }
    }

    bool CheckIfWasdIsPressed()
    {
        bool isWPressed = Input.GetKey(KeyCode.W);
        bool isAPressed = Input.GetKey(KeyCode.A);
        bool isSPressed = Input.GetKey(KeyCode.S);
        bool isDPressed = Input.GetKey(KeyCode.D);

        return isWPressed || isAPressed || isSPressed || isDPressed;
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * playerMoveSpeed * Time.deltaTime);
        }
    }

    void UpdateMovementAnimation()
    {
        bool isWASDpressed = CheckIfWasdIsPressed();
        bool isLeftShiftPressed = Input.GetKey("left shift");
        bool isWalking = animator.GetBool("isWalking");
        bool isRunning = animator.GetBool("isRunning");

        if (!isWalking && isWASDpressed)
        {
            animator.SetBool("isWalking", true);
        }

        if (isWalking && !isWASDpressed)
        {
            animator.SetBool("isWalking", false);
        }

        if (!isRunning && (isLeftShiftPressed && isWASDpressed))
        {
            playerMoveSpeed = playerMoveSpeed + 2;
            animator.SetBool("isRunning", true);
        }

        if (isRunning && (!isLeftShiftPressed || !isWASDpressed))
        {
            playerMoveSpeed = playerMoveSpeed - 2;
            animator.SetBool("isRunning", false);
        }
    }

    void ResetMovementAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
    }

    bool AreMenusActive()
    {
        GameObject[] menus = GameObject.FindGameObjectsWithTag("Menu");

        foreach (var menu in menus)
        {
            if (menu.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }
}
