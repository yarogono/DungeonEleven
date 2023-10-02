using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : Animations
{
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private static readonly int attack = Animator.StringToHash("attack");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int isJump = Animator.StringToHash("isJump");
    private static readonly int isFall = Animator.StringToHash("isFall");
    
    private Rigidbody2D _rigidbody;


    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>(); 
    }
    void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;
        controller.OnJumpEvent += Jump;
        controller.OnFallEvent += Fall;

        //if (_healthSystem != null)
        //{
        //    _healthSystem.OnDamage += Hit;
        //    _healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        //}
    }


    private void Move(Vector2 obj)
    {
        animator.SetBool(isWalking, obj.magnitude > .5f);
    }

    private void Jump()
    {
        animator.SetBool(isJump, _rigidbody.velocity.y > 0.01f); ;
    }

     private void Fall()
    {
        
        animator.SetBool(isFall,_rigidbody.velocity.y<0) ;
    }

    private void Attacking(AttackSO obj)
    {
        animator.SetTrigger(attack);
    }

    private void Hit()
    {
        animator.SetBool(isHit, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(isHit, false);
    }



}
