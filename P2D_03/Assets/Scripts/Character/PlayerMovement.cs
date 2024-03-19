using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Character character;
    public Animator animator;

    public Vector2 speed = new Vector2();
    public Vector2 moveDir = new Vector2();
    public float gravity = 25f;

    public void Start()
    {
        character.Animator.SetBool("Ready", true);
    }

    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var direction = new Vector2(horizontal, vertical);

        Move(direction);
    }

    public void Move(Vector2 direction)
    {
        if (controller.isGrounded)
        {
            moveDir = new Vector3(speed.x * direction.x, speed.y * direction.y);

            if (direction != Vector2.zero)
            {
                Turn(moveDir.x);
            }
        }

        if (controller.isGrounded)
        {
            if (direction != Vector2.zero)
            {
                character.SetState(CharacterState.Run);
            }
            else if (character.GetState() < CharacterState.DeathB)
            {
                character.SetState(CharacterState.Idle);
            }
        }
        else
        {
            character.SetState(CharacterState.Jump);
        }

        moveDir.y -= gravity * Time.deltaTime; // Depends on project physics settings
        controller.Move(moveDir * Time.deltaTime);
    }

    public void Turn(float direction)
    {
        animator.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}
