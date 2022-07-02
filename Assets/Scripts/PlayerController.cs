using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    Vector2 input;
    public Vector3 move;
    private string SPEED = "speed";
    private string ISHARVESTING = "isHarvesting";
    public bool isTouchingWheat;

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
        move = new Vector3(input.x, 0, input.y);
        if (input != Vector2.zero)
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void SetAnimationValues()
    {
        anim.SetFloat(SPEED, move.magnitude);
        anim.SetBool(ISHARVESTING, isHarvesting);
        if (!anim.GetBool(ISHARVESTING))
            isTouchingWheat = false;
    }
    public void TouchingWheatSwitch()
    {
        isTouchingWheat = true;
    }
}