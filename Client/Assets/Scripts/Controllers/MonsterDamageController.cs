using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageController : MonoBehaviour
{
    [SerializeField] public float maxHP;
    [SerializeField] private float _atk;
    private HealthSystem _playerHealthSystem;
    private PlayerController _playerController;
    private MonsterController _monsterController;
    public float currentHP;

    private void Awake()
    {
        _playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        _playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _monsterController = GetComponent<MonsterController>();
    }

    private void Start()
    {
        currentHP = maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player") && _playerHealthSystem != null && Mathf.Abs(collision.transform.position.x - transform.position.x) <= 1.5f)
        {
            if(_monsterController.isAttack == false)
            {
                _playerHealthSystem.ChangeHealth(-_atk);
                Debug.Log(_playerHealthSystem.CurrentHealth);
            }
            else if(_monsterController.isAttack)
            {
                _playerHealthSystem.ChangeHealth(-_atk * 2);
                Debug.Log(_playerHealthSystem.CurrentHealth);
            }
            _playerController.CallHitEvent();
        }
    }

    public void AttackMonster(float damage)
    {
        currentHP  -= damage;
        currentHP = (currentHP < 0)? 0 : currentHP;
        if (currentHP <= 0f)
        {
            _monsterController.CallDeathEvent();
        }
    }
}
