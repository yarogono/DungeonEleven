using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfDungeon : MonoBehaviour
{
    private HealthSystem _playerHealthSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerHealthSystem = collision.gameObject.GetComponent<HealthSystem>();
        if( _playerHealthSystem != null )
            _playerHealthSystem.ChangeHealth(-99999);
    }

}
