using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class BossController : MonsterController
{
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(MonsterBehaviour());
    }

    protected override IEnumerator MonsterBehaviour()
    {
        while (!isDead)
        {
            _animator.SetBool(_isMove, false);
            _animator.SetBool(_isAttack, false);
            isAttack = false;
            float moveRate = 1.0f / MoveSpeed;
            _behaviourSelector = UnityEngine.Random.Range(0, 10);
            /*
             * ���� Ȯ���� �ൿ ����(IDLE, LEFT, RIGHT)
             * LEFT ���� Ư�� Ƚ�� �̵����� �� LEFT ���� �ȵ�. �߰����� �� ������ IDLE �Ǵ� RIGHT ����
             * �÷��̾� �ν� �� �÷��̾� �������� �̵� �Ǵ� ����
             */
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
                behaviourType = (_behaviourSelector < 5) ? BehaviourType.MOVE : BehaviourType.ATTACK;
            }

            switch (behaviourType)
            {
                case BehaviourType.LEFT:
                    transform.localScale = new Vector3(-1, 1, 1);
                    _animator.SetBool(_isMove, true);
                    transform.DOMoveX(positionIndicator, moveRate);
                    break;
                case BehaviourType.RIGHT:
                    transform.localScale = new Vector3(1, 1, 1);
                    _animator.SetBool(_isMove, true);
                    transform.DOMoveX(positionIndicator, moveRate);
                    break;
                case BehaviourType.IDLE:
                    _animator.SetBool(_isMove, false);
                    break;
                case BehaviourType.MOVE:
                    _animator.SetBool(_isMove, true);
                    MoveTowardsPlayer(moveRate);
                    break;
                case BehaviourType.ATTACK:
                    _animator.SetTrigger("Attack");
                    isAttack = true;
                    MoveTowardsPlayer(moveRate);
                    break;
            }

            yield return new WaitForSecondsRealtime(moveRate);
        }
    }
}
