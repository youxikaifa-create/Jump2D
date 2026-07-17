using UnityEngine;

public class SimpleRay2D : MonoBehaviour
{
    public float rayLength = 3f;
    public LayerMask targetLayer;

    void Update()
    {
        // 发射向右的2D单线射线
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.right,
            rayLength,
            targetLayer
        );

        if (hit)
        {
            // 碰到目标，红线
            Debug.DrawLine(transform.position, hit.point, Color.red);
            Debug.Log("射线击中物体：" + hit.collider.name);
            Destroy(hit.collider.gameObject);
        }
        else
        {
            // 没碰到，绿线
            Vector2 endPos = (Vector2)transform.position + Vector2.right * rayLength;
            Debug.DrawLine(transform.position, endPos, Color.green);
        }
    }
}