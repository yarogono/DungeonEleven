using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : PlayerController
{
    private Camera _camera;
    [SerializeField] private SpriteRenderer characterRenderer;
    Rigidbody2D rigid;



    protected override void Awake()
    {
        base.Awake(); 
        _camera = Camera.main;
        rigid = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        //Debug.Log("OnLook" + value.ToString());
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;
        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }

    }

    public void OnAttack(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("점프키 입력받음 : " + value);
        IsJump = value.isPressed;
    }

}
