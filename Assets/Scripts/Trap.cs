using UnityEngine;
using UnityEngine.SceneManagement;

public class Trap : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool moveRight = true;

    void Update()
    {
        if (moveRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);//速度*时间增量，使移动与帧率无关
            if (transform.position.x >= rightPoint.position.x)
            {
                moveRight = false;
                FlipEnemy();
            }
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= leftPoint.position.x)
            {
                moveRight = true;
                FlipEnemy();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新开始游戏
        }
    }

    //敌人转身
    void FlipEnemy()//转向方法
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}