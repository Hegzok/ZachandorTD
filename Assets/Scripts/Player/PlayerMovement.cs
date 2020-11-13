using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Texture2D crosshair; // Delete
    [SerializeField] private DynamicStats dynamicStats;

    private PlayerInputActions playerInputActions;
    private CharacterController characterController;

    private Vector3 velocity;

    [SerializeField]
    private float horizontal;
    [SerializeField]
    private float vertical;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(crosshair, new Vector2(16, 16), CursorMode.Auto);

        dynamicStats = DataStorage.Player.DynamicStats;

        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsMouse();
        Move();
        //HandleHotkeys();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();

        playerInputActions.Land.MoveHorizontal.performed += x => horizontal = x.ReadValue<float>();
        playerInputActions.Land.MoveVertical.performed += x => vertical = x.ReadValue<float>();
        
    }

    private void OnDisable()
    {
        playerInputActions.Disable();

        playerInputActions.Land.MoveHorizontal.performed -= x => horizontal = x.ReadValue<float>();
        playerInputActions.Land.MoveVertical.performed -= x => vertical = x.ReadValue<float>();
    }

    private void Move()
    {
        Vector3 move = new Vector3(horizontal, 0f, vertical);

        Vector3 localVel = transform.InverseTransformDirection(move);

        if (localVel.z < (-0.3f))
        {
            characterController.Move(move.normalized * dynamicStats.CurrentMovementSpeedBackwards * Time.deltaTime);
        }
        else
        {
            characterController.Move(move.normalized * dynamicStats.CurrentMovementSpeed * Time.deltaTime);
        }

        animator.SetFloat("VelX", localVel.x);
        animator.SetFloat("VelY", localVel.z);

        characterController.Move(velocity * Time.deltaTime);
    }

    private void RotateTowardsMouse()
    {
        transform.LookAt(DataStorage.MouseInfo.ReturnMousePos(this.transform));
    }

    private void HandleHotkeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EventsManager.CallOnHotkeyChosen(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EventsManager.CallOnHotkeyChosen(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EventsManager.CallOnHotkeyChosen(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EventsManager.CallOnHotkeyChosen(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EventsManager.CallOnHotkeyChosen(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EventsManager.CallOnHotkeyChosen(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EventsManager.CallOnHotkeyChosen(6);
        }
    }

}
