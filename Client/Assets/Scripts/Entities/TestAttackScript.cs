using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttackScript : MonoBehaviour
{
    private PlayerController _playerController;
    private AttackSO _playerAttackSO;
    [SerializeField] private GameObject _attackGameObject;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        _playerController.OnAttackEvent += ActivateHitBox;
        _playerAttackSO = GetComponent<PlayerStatsHandler>().CurrentStates.attackSO;
    }

    private void ActivateHitBox(AttackSO attackSO)
    {
        Debug.Log("АјАн!");
        _attackGameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_attackGameObject.activeSelf&&collision.CompareTag("Monster"))
        {
            _attackGameObject.SetActive(false);
            collision.GetComponent<MonsterDamageController>().AttackMonster(_playerAttackSO.power);
            collision.GetComponent<MonsterController>().animator.SetTrigger("Hit");
        }
    }
}
