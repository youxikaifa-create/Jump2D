using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;//接受玩家对象的子对象GroundCheck的Transform组件
    public LayerMask groundLayer;//需要将地面的Layer设置为Ground
    private int jumpCount = 2;
    public int maxJump = 2;
    private bool wasGround;
    private Rigidbody2D rb;
    private Animator _anim;

    //移动端
    float moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);//圆心和半径
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpCount++;
        }

        if (Input.GetButtonDown("Jump"))
        {
            EventSystem.current.SetSelectedGameObject(null);//去掉空格按下按钮生效的错误情况
        }


        //if (Input.GetButtonDown("Jump") && IsGrounded())//空格键
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //}//原本只能跳一次的代码

        if (Input.GetKeyDown(KeyCode.Alpha1))//按下1瞬间到某个位置
        {
            transform.position = new Vector3(30.5f, 0.5f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = new Vector3(90, 2, 0);
        }

        if (Mathf.Approximately(Input.GetAxisRaw("Horizontal"),0) || !IsGrounded())
        {
            _anim.SetBool("isWalking", false);
        }
        else
        {
            _anim.SetBool("isWalking", true);
        }
        _anim.SetFloat("xDir", Input.GetAxisRaw("Horizontal"));
    }

    void FixedUpdate()
    {
        if (IsGrounded() && !wasGround)//确保只有主角落地那一刻执行重置
         jumpCount = 0;
        wasGround = IsGrounded();

        // 左右移动
        moveDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveDir * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()// 跳跃检测方法
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }//将groundCheck.position距离groundLayer0.1f的圆形位置以内设置为接触地面

    //移动端左右移动
    #region 手机触屏UI按钮 公开方法
    // 向左移动
    public void MoveLeft()
    {
        moveDir = -1;
    }

    // 向右移动
    public void MoveRight()
    {
        moveDir = 1;
    }

    // 停止移动
    public void StopMove()
    {
        moveDir = 0;
    }

    // 跳跃
    public void Jump()
    {
        if (jumpCount < maxJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
    #endregion
}