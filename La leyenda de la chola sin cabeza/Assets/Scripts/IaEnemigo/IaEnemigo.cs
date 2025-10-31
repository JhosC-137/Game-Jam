using UnityEngine;

public class IaEnemigo : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private float patrolInterval = 3f;

    private Vector2 patrolTarget;
    private float patrolTimer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolTimer = patrolInterval;
        SetNewPatrolTarget();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        patrolTimer -= Time.deltaTime;

        Vector2 direction = (patrolTarget - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolTarget) < 0.5f || patrolTimer <= 0f)
        {
            SetNewPatrolTarget();
            patrolTimer = patrolInterval;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * chaseSpeed * Time.deltaTime);
    }

    void SetNewPatrolTarget()
    {
        float randX = Random.Range(-10f, 10f); // ajusta según tu mapa
        float randY = Random.Range(-10f, 10f);
        patrolTarget = new Vector2(randX, randY);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}