using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [Header("移动距离")]
    public float range = 4f;
    [Header("移动速度")]
    public float speed = 2.2f;

    private Rigidbody2D rb;
    private Vector2 startPos;
    private Vector2 targetPos;
    private bool toRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        targetPos = new Vector2(startPos.x + range, startPos.y);
    }

    void FixedUpdate()
    {
        MoveLogic();
    }

    void MoveLogic()
    {
        Vector2 dest = toRight ? targetPos : startPos;
        rb.MovePosition(Vector2.MoveTowards(rb.position, dest, speed * Time.fixedDeltaTime));

        float distance = Mathf.Abs(rb.position.x - dest.x);
        if (distance < 0.05f)
        {
            toRight = !toRight;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector2 end = new Vector2(startPos.x + range, startPos.y);
        Gizmos.DrawLine(startPos, end);
    }
}