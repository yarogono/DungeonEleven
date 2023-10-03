using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.TestTools;
using static Define;

public class BossController : MonsterController
{
    private const string pattern1 = "Attack";
    private const string pattern2 = "Attack 2";
    private const string pattern3 = "Attack 3";
    private float _maxHP;
    private float _currentHP;
    private MonsterDamageController _damageController;
    private BossPhase _phase;
    private void Awake()
    {
        _damageController = GetComponent<MonsterDamageController>();
        _player = GameObject.FindGameObjectWithTag("Player");
        MonsterDeathEvent += ItemManager.Instance.CreateItem;
        MonsterDeathEvent += OnMonsterDeath;
    }

    private void Start()
    {
        _maxHP = _damageController.maxHP;
        StartCoroutine(MonsterBehaviour());
    }

    protected override IEnumerator MonsterBehaviour()
    {
        while (!isDead)
        {
            _currentHP = _damageController.currentHP;
            UnityEngine.Debug.Log("보스 남은 체력"+ _currentHP);
            animator.SetBool(_isMove, false);
            isAttack = false;
            float moveRate = 1.0f / MoveSpeed;
            _behaviourSelector = UnityEngine.Random.Range(0, 10);

            float hpPercentage = _currentHP / _maxHP;
            /*
             * 일정 확률로 행동 선택(IDLE, LEFT, RIGHT)
             * LEFT 으로 특정 횟수 이동했을 시 LEFT 선택 안됨. 중간값이 될 때까찌 IDLE 또는 RIGHT 선택
             * 플레이어 인식 시 플레이어 방향으로 이동 또는 공격
             * 남은 HP 비율에 따라 보스 Phase 결정
             * Phase에 따라 보스 행동 패턴 변경
             */
            if(hpPercentage > 0.7f)
            {
                _phase = BossPhase.Phase1;
            }
            else if(hpPercentage > 0.3f && hpPercentage <= 0.7f)
            {
                _phase = BossPhase.Phase2;
            }
            else
            {
                _phase = BossPhase.Phase3;
            }

            if (!playerDetected)
            {
                if (_behaviourSelector < 4 && positionIndicator > -MoveRange)
                {
                    behaviourType = BehaviourType.LEFT;
                    positionIndicator--;
                }
                else if (_behaviourSelector < 7 && positionIndicator < MoveRange)
                {
                    behaviourType = BehaviourType.RIGHT;
                    positionIndicator++;
                }
                else
                {
                    behaviourType = BehaviourType.IDLE;
                }
            }
            else
            {
                switch (_phase)
                {
                    case BossPhase.Phase1:
                        behaviourType = (_behaviourSelector < 5) ? BehaviourType.MOVE : BehaviourType.ATTACK;
                        break;
                    case BossPhase.Phase2:
                        behaviourType = (_behaviourSelector < 5) ? BehaviourType.MOVE : BehaviourType.ATTACK2;
                        break;
                    case BossPhase.Phase3:
                        behaviourType = (_behaviourSelector < 5) ? BehaviourType.ATTACK2 : BehaviourType.ATTACK3;
                        break;
                }
            }              

            switch (behaviourType)
            {
                case BehaviourType.LEFT:
                    transform.localScale = new Vector3(-1, 1, 1);
                    animator.SetBool(_isMove, true);
                    transform.DOMoveX(positionIndicator, moveRate);
                    break;
                case BehaviourType.RIGHT:
                    transform.localScale = new Vector3(1, 1, 1);
                    animator.SetBool(_isMove, true);
                    transform.DOMoveX(positionIndicator, moveRate);
                    break;
                case BehaviourType.IDLE:
                    animator.SetBool(_isMove, false);
                    break;
                case BehaviourType.MOVE:
                    animator.SetBool(_isMove, true);
                    MoveTowardsPlayer(moveRate);
                    break;
                case BehaviourType.ATTACK:
                    animator.SetTrigger(pattern1);
                    isAttack = true;
                    MoveTowardsPlayer(moveRate);
                    break;
                case BehaviourType.ATTACK2:
                    animator.SetTrigger(pattern2);
                    isAttack = true;
                    MoveTowardsPlayer(moveRate);
                    break;
                case BehaviourType.ATTACK3:
                    animator.SetTrigger(pattern3);
                    isAttack = true;
                    MoveTowardsPlayer(moveRate);
                    break;
            }

            yield return new WaitForSecondsRealtime(moveRate);
        }
    }
    protected override void OnMonsterDeath(Vector3 position)
    {
        //TODO : 보스가 죽은 뒤 실행해야 할 기능들 추가하기(UI 띄우기 등)
        isDead = true;
        animator.SetTrigger(_isDead);
        Destroy(gameObject);
    }
}
