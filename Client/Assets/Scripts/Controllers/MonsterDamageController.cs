using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamageController : MonoBehaviour
{
    [SerializeField] private float _maxHP;
    [SerializeField] private float _atk;
    private HealthSystem _playerHealthSystem;
    private MonsterController _monsterController;
    private float _currentHP;

    private void Awake()
    {
        _playerHealthSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthSystem>();
        _monsterController = GetComponent<MonsterController>();
    }

    private void Start()
    {
        _currentHP = _maxHP;
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
        }
    }
}
