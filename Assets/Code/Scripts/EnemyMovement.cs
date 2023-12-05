using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform target;
    private int pathIndex = 0;
    public GameObject enemyPrefab;
    private float baseSpeed;

    public int damage = 10;
    // Start is called before the first frame update
    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnermySpawner.onEnemyDestroy.Invoke();
                EndPath();
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
        
        if (pathIndex > 5)
        {
            Debug.Log("X-value increased, flipping enemy!");
            transform.localScale = new Vector2(.1f, .1f);
        }
        else if (pathIndex <= 5)
        {
            Debug.Log("X-value decreased, flipping enemy!");
            transform.localScale = new Vector2(-.1f, .1f);
        }
    }
    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    void EndPath()
    {
        UserHealth.health -= damage;
        Destroy(gameObject);
    }
}
