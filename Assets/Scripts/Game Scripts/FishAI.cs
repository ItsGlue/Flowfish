using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FishAI : MonoBehaviour
{
    public float minMoveSpeed = 1f;
    public float maxMoveSpeed = 3f;
    public float moveSpeed = 1f;
    public float changeDirectionTime = 3f;
    public float turnSpeed = 2f;
    public float wallAvoidanceDistance = 1f;
    public float cooldownTime = 0.2f;

    public float hunger = 100f;
    public float hungerDecreaseRate = 1f;
    public float hungerThreshold = 30f;
    public float hungerReplenishAmount = 50f;


    public LayerMask wallLayer;
    public LayerMask foodLayer;

    public Slider hungerSlider;

    private Vector2 targetDirection;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isStuck = false;
    private float lastWallHitTime = -Mathf.Infinity;
    private GameObject targetFood = null;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirectionRoutine());
        StartCoroutine(CheckForStuckRoutine());
        StartCoroutine(ChangeSpeedRoutine());
        StartCoroutine(HungerDecreaseRoutine());
    }

    void Update()
    {
        if (targetFood != null)
        {
            MoveTowardsFood();
        }
        else
        {
            SmoothMove();
        }

        hungerSlider.value = hunger/100;
        RotateTowardsMoveDirection();
        FlipHorizontallyIfNeeded();
    }

    void SmoothMove()
    {
        Vector2 smoothDirection = Vector2.Lerp(rb.velocity, targetDirection, turnSpeed * Time.deltaTime);
        rb.velocity = smoothDirection;
    }

    void MoveTowardsFood()
    {
        if (targetFood != null)
        {
            Vector2 directionToFood = (targetFood.transform.position - transform.position).normalized;
            rb.velocity = directionToFood * moveSpeed;
            if (Vector2.Distance(transform.position, targetFood.transform.position) < 0.1f) // check distance to food
            {
                Destroy(targetFood);
                hunger = Mathf.Min(100f, hunger + hungerReplenishAmount);
                targetFood = null;
                Debug.Log("Food eaten, hunger replenished.");
            }
        }
    }

    void RotateTowardsMoveDirection()
    {
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FlipHorizontallyIfNeeded()
    {
        if (targetFood == null)
        {
            if (rb.velocity.x > 0 && !facingRight)
            {
                FlipHorizontally();
            }
            else if (rb.velocity.x < 0 && facingRight)
            {
                FlipHorizontally();
            }
        }
    }

    void FlipHorizontally()
    {
        facingRight = !facingRight;
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            if (!isStuck && !IsNearWall() && targetFood == null)
            {
                targetDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                targetDirection *= moveSpeed;
            }
            yield return new WaitForSeconds(changeDirectionTime);
        }
    }

    IEnumerator ChangeSpeedRoutine()
    {
        while (true)
        {
            if (!isStuck && !IsNearWall() && targetFood == null)
            {
                moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    IEnumerator CheckForStuckRoutine()
    {
        while (true)
        {
            Vector2 previousPosition = transform.position;
            yield return new WaitForSeconds(2f);

            if (Vector2.Distance(previousPosition, transform.position) < 0.1f)
            {
                isStuck = true;
                ForceChangeDirection();
            }
            else
            {
                isStuck = false;
            }
        }
    }

    void ForceChangeDirection()
    {
        targetDirection = -targetDirection + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)).normalized;
        targetDirection *= moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("fishfood"))
        {
            Destroy(collision.gameObject);
            hunger = Mathf.Min(100f, hunger + hungerReplenishAmount);
            targetFood = null;
        }

        if (Time.time - lastWallHitTime > cooldownTime)
        {
            lastWallHitTime = Time.time;
            targetDirection = -targetDirection + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)).normalized;
        }
    }

    bool IsNearWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, wallAvoidanceDistance, wallLayer);
        return hit.collider != null;
    }

    IEnumerator HungerDecreaseRoutine()
    {
        while (true)
        {
            hunger -= hungerDecreaseRate;
            if (hunger <= hungerThreshold && targetFood == null)
            {
                Collider2D foodCollider = Physics2D.OverlapCircle(transform.position, 5f, foodLayer);
                if (foodCollider != null)
                {
                    targetFood = foodCollider.gameObject;
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
