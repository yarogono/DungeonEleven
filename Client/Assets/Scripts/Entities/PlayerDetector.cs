using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private MonsterController _monsterController;

    private void Awake()
    {
        _monsterController = transform.GetComponentInParent<MonsterController>();
    }

    private void Start()
    {
        _monsterController.PlayerDetectEvent += StartLookingPlayer;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _monsterController.DetectPlayer();
            _monsterController.CallDetectEvent(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _monsterController.AwayPlayer();
        StopCoroutine(GetPlayerPosition(collision));
    }

    private void StartLookingPlayer(Collider2D player)
    {
        StartCoroutine(GetPlayerPosition(player));
    }
    private IEnumerator GetPlayerPosition(Collider2D player)
    {
        while(true)
        {
            _monsterController.playerPosition = player.transform.localPosition;
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
