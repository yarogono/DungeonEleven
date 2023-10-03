using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnJumpEvent;
    public event Action OnHitEvent;
    public event Action OnFallEvent;
    public event Action<Vector2> OnDashEvent;
    public event Action<AttackSO> OnAttackEvent;

    private float _timeSinceLastAttack = float.MaxValue;

    protected bool IsAttacking { get; set; }
    protected bool IsJump { get; set; }

    protected bool IsJumping { get; set; }
    protected PlayerStatsHandler Stats { get; private set; }

    float jumpingSet;

    protected virtual void Awake()
    {
        Stats = GetComponent<PlayerStatsHandler>();
        
    }

    private void Start()
    {
        jumpingSet = gameObject.GetComponent<PlayerStatsHandler>().CurrentStates.jumpingPower;
        //Debug.Log("jumpingSet : " + jumpingSet);
    }


    protected virtual void Update()
    {
        HandleAttackDelay();
        jumpDelay();
    }

    private void HandleAttackDelay()
    {
        if (Stats.CurrentStates.attackSO == null) return;

        if (_timeSinceLastAttack <= Stats.CurrentStates.attackSO.delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        if (IsAttacking && _timeSinceLastAttack > Stats.CurrentStates.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            CallAttackEvent(Stats.CurrentStates.attackSO);
            IsAttacking = false;
        }
    }

    private void jumpDelay()
    {
        if (IsJumping == true) return;
        else if (IsJumping == false)
        { 
            IsJumping = true;
            CallJumpEvent();
           
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
       // Debug.Log("이벤트 확인 CallMoveEvent");
        OnMoveEvent?.Invoke(direction);
    }
    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }
    public void CallJumpEvent()
    {
        OnJumpEvent?.Invoke();
    }
    public void CallHitEvent()
    {
        OnHitEvent?.Invoke();
    }
    public void CallFallEvent()
    {
        OnFallEvent?.Invoke();
    }
    public void CallAttackEvent(AttackSO attackSO)
    {
        OnAttackEvent?.Invoke(attackSO);
    }
    public void CallDashEvent(Vector2 direction)
    {
        OnDashEvent?.Invoke(direction);
    }
}
