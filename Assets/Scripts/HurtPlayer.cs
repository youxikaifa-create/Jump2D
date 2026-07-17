using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public float damage = 20;
    private bool canHurt = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && canHurt)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);//注意标准写法
            canHurt = false;
            Invoke(nameof(OpenHurt), 1f); //1秒无敌防连续掉血
        }
    }

    void OpenHurt()
    {
        canHurt = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CancelInvoke(nameof(OpenHurt));
            canHurt = true;
        }
    }
}