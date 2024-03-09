using Assets.HeroEditor.Common.CharacterScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController Controller;
    public Character Character;

    public Vector2 speed = new Vector2();
    public Vector2 moveDir = new Vector2();
    public float gravity = 25f;

    public void Start()
    {
        Character.Animator.SetBool("Ready", true);
    }

    public void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var direction = new Vector2(horizontal, vertical);

        Move(direction);

        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    Character.SetState(CharacterState.DeathB);
        //}
    }

    public void Move(Vector2 direction)
    {
        if (Controller.isGrounded)
        {
            moveDir = new Vector3(speed.x * direction.x, speed.y * direction.y);

            if (direction != Vector2.zero)
            {
                Turn(moveDir.x);
            }
        }

        if (Controller.isGrounded)
        {
            if (direction != Vector2.zero)
            {
                Character.SetState(CharacterState.Run);
            }
            else if (Character.GetState() < CharacterState.DeathB)
            {
                Character.SetState(CharacterState.Idle);
            }
        }
        else
        {
            Character.SetState(CharacterState.Jump);
        }

        moveDir.y -= gravity * Time.deltaTime; // Depends on project physics settings
        Controller.Move(moveDir * Time.deltaTime);
    }

    public void Turn(float direction)
    {
        Character.transform.localScale = new Vector3(Mathf.Sign(direction), 1, 1);
    }
}
