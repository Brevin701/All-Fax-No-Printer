using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<UserHealth>())
        {
            
            EnermySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
}
