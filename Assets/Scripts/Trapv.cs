using UnityEngine;
using UnityEngine.SceneManagement;

public class Trapv : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public Transform upPoint;
    public Transform downPoint;
    private bool moveRight = true;

    void Update()
    {
        if (moveRight)
        {
            transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);//速度*时间增量，使移动与帧率无关
            if (transform.position.y >= upPoint.position.y)
            {
                moveRight = false;
                FlipEnemy();
            }
        }
        else
        {
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
            if (transform.position.y <= downPoint.position.y)
            {
                moveRight = true;
                FlipEnemy();
            }
        }
    }

    //敌人转身
    void FlipEnemy()//转向方法
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
    }
}