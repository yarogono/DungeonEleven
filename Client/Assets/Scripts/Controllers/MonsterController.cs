using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BehaviourType
{
    IDLE,
    LEFT,
    RIGHT,
    MOVE,
    ATTACK
}

public class MonsterController : MonoBehaviour
{
    public event Action<Vector3> MonsterDeathEvent;
    public event Action<Collider2D> PlayerDetectEvent;
    public Vector3 playerPosition = Vector3.zero;
    public bool isDead = false;
    [SerializeField] private Animator _animator;
    [SerializeField][Range(0.5f, 2.0f)] private float MoveSpeed;
    [SerializeField][Range(1, 4)] private float MoveRange;
    private GameObject _player;
    private const string _isMove = "isMove";
    private const string _isJump = "isJump";
    private const string _isAttack = "isAttack";
    private const string _isStun = "isStun";
    private int _behaviourSelector = 0;
    protected BehaviourType behaviourType;
    private bool playerDetected = false;
    private bool isRight = false;
    private int positionIndicator = 0;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        StartCoroutine(MonsterBehaviour());
    }

    public void CallDeathEvent()
    {
        MonsterDeathEvent?.Invoke(transform.position);
    }

    public void CallDetectEvent(Collider2D player)
    {
        PlayerDetectEvent?.Invoke(player);
    }
    protected IEnumerator MonsterBehaviour()
    {
        while(!isDead)
        {
            _animator.SetBool(_isMove, false);
            _animator.SetBool(_isAttack, false);
            float moveRate = 1.0f / MoveSpeed;
            _behaviourSelector = UnityEngine.Random.Range(0, 10);
            /*
             * 일정 확률로 행동 선택(IDLE, LEFT, RIGHT)
             * LEFT 으로 특정 횟수 이동했을 시 LEFT 선택 안됨. 중간값이 될 때까찌 IDLE 또는 RIGHT 선택
             * 플레이어 인식 시 플레이어 방향으로 이동 또는 공격
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
                    _animator.SetBool(_isAttack, true);
                    MoveTowardsPlayer(moveRate);
                    break;
            }

            yield return new WaitForSecondsRealtime(moveRate);
        }
    }

    private void MoveTowardsPlayer(float moveRate)
    {
        if (playerPosition.x > transform.localPosition.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.DOMoveX(playerPosition.x, moveRate * 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.DOMoveX(playerPosition.x, moveRate * 1.5f);
        }
    }

    public void DetectPlayer()
    {
        playerDetected = true;
    }
    
    public void AwayPlayer()
    {
        playerDetected = false;
    }
}
