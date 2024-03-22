using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Character character;
    public Animator animator;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed;
    private Vector3 direction;


    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3.0f;
    private float velocity;

    [SerializeField] private float jumpPower;

    private void Update()
    {
        ApplyGravity();
        HandleMove();
        HandleJump();
        ApplyMovement();

        TurnModel(direction.x);
    }

    private void ApplyGravity()
    {
        if (IsGrounded() && velocity < 0.0f)
            velocity = -1.0f;
        else
            velocity += gravity * gravityMultiplier * Time.deltaTime;

        direction.y = velocity;
    }

    private void ApplyMovement()
    {
        characterController.Move(direction * Time.deltaTime);
    }

    public void HandleMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        direction.x = horizontal * speed;

        if (horizontal != 0 && IsGrounded())
            character.SetState(CharacterState.Run);
        else if (character.GetState() < CharacterState.DeathB)
            character.SetState(CharacterState.Idle);
    }

    public void HandleJump()
    {
        if (!IsGrounded())
        {
            character.SetState(CharacterState.Jump);
            return;
        }

        bool isJump = Input.GetKeyDown(KeyCode.W);
        if (!isJump) return;


        velocity += jumpPower;
    }

    private bool IsGrounded() => characterController.isGrounded;


    public void TurnModel(float direction)
    {
        if (direction == 0) return;
        animator.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}
