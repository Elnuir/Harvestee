using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public bool isHarvesting;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] float playerSpeed = 2.0f;
    private Animator anim;
    private PlayerInput playerInput;
    [SerializeField] float rotationSpeed;
    Vector2 input, inputKeyboard;
    public Vector3 move, moveKeyboard;
    private string SPEED = "speed";
    private string ISHARVESTING = "isHarvesting";
    public bool isTouchingWheat;
    bool isRemoteConnected;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        
    }

    void Update()
    {
        SetAnimationValues();
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        input = playerInput.actions["Move"].ReadValue<Vector2>();
        inputKeyboard = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        move = new Vector3(input.x, 0, input.y);
        moveKeyboard = new Vector3(inputKeyboard.x, 0, inputKeyboard.y);
#if UNITY_EDITOR
        if (EditorApplication.isRemoteConnected)
            isRemoteConnected = true;
#endif

        if (input != Vector2.zero && Application.platform == RuntimePlatform.Android || isRemoteConnected)// EditorApplication.isRemoteConnected)
        {

            controller.Move(move * Time.deltaTime * playerSpeed);
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }


        if (inputKeyboard != Vector2.zero && Application.platform == RuntimePlatform.WindowsEditor && !isRemoteConnected)
        {

            controller.Move(moveKeyboard.normalized * Time.deltaTime * playerSpeed);
            Quaternion toRotation = Quaternion.LookRotation(moveKeyboard, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        
    }
    void SetAnimationValues()
    {

        //if(input > 0)
        // if(move.magnitude >= 0.1f)
        if (Application.platform == RuntimePlatform.Android || isRemoteConnected)
        {
            anim.SetFloat(SPEED, move.magnitude);
            print("anroid");
        }

        if (Application.platform == RuntimePlatform.WindowsEditor && !isRemoteConnected)
        {
            anim.SetFloat(SPEED, moveKeyboard.magnitude);
            print("windows");
        }

        //if(inputKeyboard.magnitude != 0)
        // if (inputKeyboard.magnitude >= 0.1f)
        //anim.SetFloat(SPEED, inputKeyboard.magnitude);
        anim.SetBool(ISHARVESTING, isHarvesting);
        if (!anim.GetBool(ISHARVESTING))
            isTouchingWheat = false;
    }
    public void TouchingWheatSwitch()
    {
        isTouchingWheat = true;
    }
}