using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform currentTransform;
    [SerializeField] private float moveSpeed;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        Move();
    }

    private void OnInit()
    {
        ChangeWeapon(WeaponType.Hammer);
        ChangeColor(ColorType.Yellow);
        ChangeColor(ColorType.Red);

        SetBullet();
    }

    private void Move()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        SetRotation(horizontal, vertical);
        SetMovement(horizontal, vertical);
    }

    private void SetRotation(float horizontal, float vertical)
    {
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);

        if (Vector3.Distance(inputDirection, Vector3.zero) > 0.1f)
        {
            Quaternion newDirection = Quaternion.LookRotation(inputDirection);
            transform.rotation = newDirection;
        }
    }

    private void SetMovement(float horizontal, float vertical)
    {
        Vector3 inputMovement = new Vector3(horizontal * moveSpeed, rb.velocity.y, vertical * moveSpeed);

        if (Vector3.Distance(inputMovement, Vector3.zero) > 0.1f)
        {
            rb.velocity = inputMovement;
            ChangeAnimRun();
        }
        else
        {
            rb.velocity = Vector3.zero;

            if (target != null)
            {
                ChangeAnimAttack();
                SetDirectionWhenAttack(this.target);
            }
            else
            {
                ChangeAnimIdle();
            }
        }
    }
}
