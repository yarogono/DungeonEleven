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
    private static readonly int isDead = Animator.StringToHash("isDead");
    


    private HealthSystem _healthSystem;

    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthSystem>();
    }
    void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;
        _healthSystem.OnDeath += Dead;
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

    private void Dead()
    {
        animator.SetBool(isDead, true);
    }



}
