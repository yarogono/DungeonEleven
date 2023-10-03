using System.Collections;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TimeOverItemDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ItemManager.Instance.LootItem(gameObject);
        }
    }

    IEnumerator TimeOverItemDestroy()
    {
        yield return new WaitForSeconds(5f);
        ItemManager.Instance.DestroyItem(gameObject);
    }
}
